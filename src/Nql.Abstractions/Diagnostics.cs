using System.Collections;
using System.Collections.Generic;
using Nql.Abstractions.Enums;

namespace Nql.Abstractions
{
    public class Diagnostics : IEnumerable<Diagnostic>
    {
        private readonly List<Diagnostic> items = new List<Diagnostic>();

        public Diagnostic this[int index] => items[index];

        public IEnumerator<Diagnostic> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void AddError(string text, NqlNodeLocation location)
        {
            items.Add(new Diagnostic(text, location, DiagnosticsLevel.Error));
        }
    }
}