using BoxCricketBuddy.Models;
using System.Collections.Generic;
using System.Linq;

namespace BoxCricketBuddy.Data
{
    public class InMemoryRepository : IRepository
    {
        private readonly List<Venue> _venues = new();
        private readonly List<Booking> _bookings = new();
        private readonly List<ContactMessage> _messages = new();

        public InMemoryRepository()
        {
            _venues = new List<Venue>
            {
                new Venue
                {
                    Id = 1,
                    Name = "Strike Zone Box Cricket",
                    Address = "Race Course Ring Road, Rajkot",
                    City = "Rajkot",
                    Rating = 4.5m,
                    Price = 800,
                    PitchType = "Indoor",
                    PitchCount = 2,
                    Description = "Premium box cricket facility with modern amenities.",
                    Contact = "+91 9876543210",
                    Images = new List<string>
                    {
                        "/images/venues/strike1.jpg",
                        "/images/venues/strike2.jpg",
                        "/images/venues/strike3.jpg"
                    },
                    Facilities = new List<string>
                    {
                        "Parking","Washroom","Changing Room","Water","First Aid","Equipment"
                    },
                    Latitude = 21.5305m,
                    Longitude = 70.8022m
                },

                new Venue
                {
                    Id = 2,
                    Name = "Champions Box Cricket Arena",
                    Address = "Kalawad Road, Rajkot",
                    City = "Rajkot",
                    Rating = 4.2m,
                    Price = 600,
                    PitchType = "Outdoor",
                    PitchCount = 3,
                    Description = "Outdoor ground with floodlights.",
                    Contact = "+91 9876543211",
                    Images = new List<string>
                    {
                        "/images/venues/champion1.jpg",
                        "/images/venues/champion2.jpg",
                        "/images/venues/champion3.jpg"
                    },
                    Facilities = new List<string>
                    {
                        "Parking","Washroom","Water","Floodlights","Equipment"
                    },
                    Latitude = 21.5236m,
                    Longitude = 70.8266m
                },

                new Venue
                {
                    Id = 3,
                    Name = "Royal Box Cricket Club",
                    Address = "University Road, Rajkot",
                    City = "Rajkot",
                    Rating = 4.7m,
                    Price = 1000,
                    PitchType = "Semi-covered",
                    PitchCount = 1,
                    Description = "Semi-covered premium facility with luxury amenities.",
                    Contact = "+91 9876543212",
                    Images = new List<string>
                    {
                        "/images/venues/royal1.jpg",
                        "/images/venues/royal2.jpg",
                        "/images/venues/royal3.jpg"
                    },
                    Facilities = new List<string>
                    {
                        "Parking","Washroom","Cafeteria","AC Waiting Area","Equipment"
                    },
                    Latitude = 21.5175m,
                    Longitude = 70.8110m
                }
            };
        }

        public IEnumerable<Venue> GetAllVenues() => _venues;

        public Venue? GetVenue(int id) =>
            _venues.FirstOrDefault(v => v.Id == id);

        public IEnumerable<Venue> Search(string city, string? pitchType, int? maxPrice)
        {
            IEnumerable<Venue> q = _venues;

            if (!string.IsNullOrWhiteSpace(city))
                q = q.Where(v => v.City.Equals(city, System.StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(pitchType))
                q = q.Where(v => v.PitchType.Equals(pitchType, System.StringComparison.OrdinalIgnoreCase));

            if (maxPrice.HasValue)
                q = q.Where(v => v.Price <= maxPrice.Value);

            return q;
        }

        public void AddBooking(Booking booking)
        {
            booking.Id = _bookings.Count + 1;
            _bookings.Add(booking);
        }

        public void AddContact(ContactMessage msg)
        {
            msg.Id = _messages.Count + 1;
            _messages.Add(msg);
        }

        public IEnumerable<ContactMessage> GetContactMessages() => _messages;
    }
}
