using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using Nql.Abstractions;

namespace Nql
{
    public class NqlTypeBuilder : INqlTypeBuilder
    {
        private static readonly AssemblyName assemblyName = new AssemblyName {Name = "NqlTypes"};

        private static readonly ModuleBuilder dynamicModule = AssemblyBuilder
            .DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run)
            .DefineDynamicModule($"{assemblyName.Name}Module");

        private static readonly Dictionary<string, Type> typeMappings = new Dictionary<string, Type>();

        private static int typeCounter;

        public Type Build(NqlField[] nqlFields, Type parent = null)
        {
            var typeHash = ComputeClassHash(nqlFields, parent);

            if (typeMappings.ContainsKey(typeHash))
                return typeMappings[typeHash];

            var typeBuilder = dynamicModule.DefineType(
                $"NqlType{typeCounter++}",
                TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Serializable,
                parent);
            var fields = AddFields(typeBuilder, nqlFields);

            AddEqualsMethod(typeBuilder, fields);
            AddGetHashCodeMethod(typeBuilder, fields);

            typeMappings.Add(typeHash, typeBuilder.CreateType());

            return typeMappings[typeHash];
        }

        private static FieldInfo[] AddFields(TypeBuilder typeBuilder, IReadOnlyList<NqlField> properties)
        {
            const MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.HideBySig;
            var returnValue = new FieldInfo[properties.Count];

            for (var index = 0; index < properties.Count; index++)
            {
                var property = properties[index];
                var name = property.Name;
                var type = property.FieldType;
                var field = typeBuilder.DefineField($"_{name}", type, FieldAttributes.Private);
                var dynamicProperty = typeBuilder.DefineProperty(name, PropertyAttributes.None, type, new[] {type});
                var getMethodBuilder = typeBuilder.DefineMethod("get_value", methodAttributes, type, Type.EmptyTypes);
                var setMethodBuilder = typeBuilder.DefineMethod("set_value", methodAttributes, null, new[] {type});
                var getIl = getMethodBuilder.GetILGenerator();
                var setIl = setMethodBuilder.GetILGenerator();

                getIl.Emit(OpCodes.Ldarg_0);
                getIl.Emit(OpCodes.Ldfld, field);
                getIl.Emit(OpCodes.Ret);

                setIl.Emit(OpCodes.Ldarg_0);
                setIl.Emit(OpCodes.Ldarg_1);
                setIl.Emit(OpCodes.Stfld, field);
                setIl.Emit(OpCodes.Ret);

                dynamicProperty.SetGetMethod(getMethodBuilder);
                dynamicProperty.SetSetMethod(setMethodBuilder);

                returnValue[index] = field;
            }

            return returnValue;
        }

        private static void AddEqualsMethod(TypeBuilder typeBuilder, IEnumerable<FieldInfo> fields)
        {
            var method = typeBuilder.DefineMethod(nameof(Equals),
                MethodAttributes.Public | MethodAttributes.ReuseSlot |
                MethodAttributes.Virtual | MethodAttributes.HideBySig,
                typeof(bool),
                new[] {typeof(object)});

            var methodGenerator = method.GetILGenerator();
            var other = methodGenerator.DeclareLocal(typeBuilder);
            var next = methodGenerator.DefineLabel();

            methodGenerator.Emit(OpCodes.Ldarg_1);
            methodGenerator.Emit(OpCodes.Isinst, typeBuilder);
            methodGenerator.Emit(OpCodes.Stloc, other);
            methodGenerator.Emit(OpCodes.Ldloc, other);
            methodGenerator.Emit(OpCodes.Brtrue_S, next);
            methodGenerator.Emit(OpCodes.Ldc_I4_0);
            methodGenerator.Emit(OpCodes.Ret);
            methodGenerator.MarkLabel(next);

            foreach (var field in fields)
            {
                var comparerType = typeof(EqualityComparer<>).MakeGenericType(field.FieldType);
                var equalsMethod = comparerType.GetMethod("Equals", new[] {field.FieldType, field.FieldType});

                next = methodGenerator.DefineLabel();
                methodGenerator.EmitCall(OpCodes.Call, comparerType.GetMethod("get_Default"), null);
                methodGenerator.Emit(OpCodes.Ldarg_0);
                methodGenerator.Emit(OpCodes.Ldfld, field);
                methodGenerator.Emit(OpCodes.Ldloc, other);
                methodGenerator.Emit(OpCodes.Ldfld, field);
                methodGenerator.EmitCall(OpCodes.Callvirt, equalsMethod, null);
                methodGenerator.Emit(OpCodes.Brtrue_S, next);
                methodGenerator.Emit(OpCodes.Ldc_I4_0);
                methodGenerator.Emit(OpCodes.Ret);
                methodGenerator.MarkLabel(next);
            }

            methodGenerator.Emit(OpCodes.Ldc_I4_1);
            methodGenerator.Emit(OpCodes.Ret);
        }

        private static void AddGetHashCodeMethod(TypeBuilder typeBuilder, IEnumerable<FieldInfo> fields)
        {
            var methodBuilder = typeBuilder.DefineMethod(nameof(GetHashCode),
                MethodAttributes.Public | MethodAttributes.ReuseSlot |
                MethodAttributes.Virtual | MethodAttributes.HideBySig,
                typeof(int),
                null);
            var ilGenerator = methodBuilder.GetILGenerator();

            ilGenerator.Emit(OpCodes.Ldc_I4_0);
            foreach (var field in fields)
            {
                var comparerType = typeof(EqualityComparer<>).MakeGenericType(field.FieldType);
                var getHashCodeMethod = comparerType.GetMethod("GetHashCode", new[] {field.FieldType});

                ilGenerator.EmitCall(OpCodes.Call, comparerType.GetMethod("get_Default"), null);
                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.Emit(OpCodes.Ldfld, field);
                ilGenerator.EmitCall(OpCodes.Callvirt, getHashCodeMethod, null);
                ilGenerator.Emit(OpCodes.Xor);
            }

            ilGenerator.Emit(OpCodes.Ret);
        }

        private static string ComputeClassHash(IEnumerable<NqlField> properties, Type parent)
        {
            var fullName = properties
                .OrderBy(i => i.Name)
                .Select((i, j) => $"{j:D4}{i.Name}{i.FieldType.FullName}")
                .Aggregate((first, second) => first + second);

            using (var sha1 = new SHA1Managed())
            {
                var hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(fullName));
                var stringBuilder = new StringBuilder(hashBytes.Length * 2);

                foreach (var hashByte in hashBytes)
                    stringBuilder.Append(hashByte.ToString("X2"));

                if (parent != null)
                    stringBuilder.Append(parent.GetHashCode().ToString("X2"));

                return stringBuilder.ToString();
            }
        }
    }
}