using System.Text.RegularExpressions;
using SQLite;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace BareLink
{
    public class Filter
    {
        private Regex _regex;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Pattern { get; set; }
        public bool Active { get; set; } = true;

        public Regex Regex => _regex ??= new Regex(Pattern);

        public bool TryMatch([NotNull] string input, out string result)
        {
            result = null;
            var match = Regex.Match(input);
            if (!match.Success) return false;
            result = match.Value;
            return true;
        }
    }
}