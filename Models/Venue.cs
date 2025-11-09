using System.Collections.Generic;

namespace BoxCricketBuddy.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = "";
        public decimal Rating { get; set; }
        public int Price { get; set; }
        public string PitchType { get; set; } = "";
        public int PitchCount { get; set; }
        public string Description { get; set; } = "";
        public string Contact { get; set; } = "";
        public IEnumerable<string> Images { get; set; } = new List<string>();
        public IEnumerable<string> Facilities { get; set; } = new List<string>();
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
