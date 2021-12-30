using System.Text.RegularExpressions;
using Newtonsoft.Json;
using SQLite;

namespace BareLink.Models
{
    public class Filter
    {
        private Regex _regex;

        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Pattern { get; set; }
        public bool Active { get; set; } = true;

        [JsonIgnore]
        public Regex Regex => _regex ??= new Regex(Pattern);

        public bool TryMatch(string input, out string result)
        {
            result = null;
            var match = Regex.Match(input);
            if (!match.Success) return false;
            result = match.Value;
            return true;
        }
    }
}