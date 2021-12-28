using SQLite;

namespace BareLink
{
    public class Filter
    {
        [PrimaryKey]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Pattern { get; set; }
        public bool Active { get; set; } = true;
    }
}