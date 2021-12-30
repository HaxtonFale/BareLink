using System.Text.RegularExpressions;
using Newtonsoft.Json;
using SQLite;
using NotNullAttribute = BareLink.Annotations.NotNullAttribute;

namespace BareLink.Models
{
    public class Filter
    {
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string Pattern
        {
            get => Regex.ToString();
            set => Regex = new Regex(value);
        }

        public bool Enabled { get; set; } = true;

        [JsonIgnore]
        [Ignore]
        public Regex Regex { get; private set; }

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