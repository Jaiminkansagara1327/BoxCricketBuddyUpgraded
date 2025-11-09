using LiteDB;
using BoxCricketBuddy.Models;
using System.Collections.Generic;
using System.Linq;

namespace BoxCricketBuddy.Data
{
    public class LiteDbRepository : IRepository
    {
        private readonly string _dbPath = "App_Data/boxcricket.db";

        public LiteDbRepository()
        {
            if (!Directory.Exists("App_Data"))
                Directory.CreateDirectory("App_Data");

            using var db = new LiteDatabase(_dbPath);

            // Ensure collections exist
            var venues = db.GetCollection<Venue>("venues");
            var bookings = db.GetCollection<Booking>("bookings");
            var messages = db.GetCollection<ContactMessage>("messages");

            // Seed venues only once
            if (venues.Count() == 0)
            {
                venues.InsertBulk(InMemorySeedData());
            }
        }

        private IEnumerable<Venue> InMemorySeedData()
        {
            return new List<Venue>
            {
                new Venue {
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
                        "https://images.unsplash.com/photo-1505685296765-3a2736de412f?q=80&w=1200",
                        "https://images.unsplash.com/photo-1599058917212-d750089bc07e?q=80&w=1200",
                        "https://images.unsplash.com/photo-1508614589041-895b88991e3e?q=80&w=1200"
                    },
                    Facilities = new List<string>{ "Parking","Washroom","Changing Room","Water","First Aid","Equipment" },
                    Latitude = 21.5305m,
                    Longitude = 70.8022m
                },
                new Venue {
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
                        "https://images.unsplash.com/photo-1521412644187-c49fa049e84d?q=80&w=1200",
                        "https://images.unsplash.com/photo-1570461182083-e0b7b1e0c6c9?q=80&w=1200",
                        "https://images.unsplash.com/photo-1574629810360-7efbbe195018?q=80&w=1200"
                    },
                    Facilities = new List<string>{ "Parking","Washroom","Water","Floodlights","Equipment" },
                    Latitude = 21.5236m,
                    Longitude = 70.8266m
                },
                new Venue {
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
                        "https://images.unsplash.com/photo-1574629810360-7efbbe195018?q=80&w=1200",
                        "https://images.unsplash.com/photo-1543357480-c60d8ccc7487?q=80&w=1200",
                        "https://images.unsplash.com/photo-1508614999368-926005129ccb?q=80&w=1200"
                    },
                    Facilities = new List<string>{ "Parking","Washroom","Cafeteria","AC Waiting Area","Equipment" },
                    Latitude = 21.5175m,
                    Longitude = 70.8110m
                }
            };
        }


        public IEnumerable<Venue> GetAllVenues()
        {
            using var db = new LiteDatabase(_dbPath);
            return db.GetCollection<Venue>("venues").FindAll().ToList();
        }

        public Venue? GetVenue(int id)
        {
            using var db = new LiteDatabase(_dbPath);
            return db.GetCollection<Venue>("venues").FindById(id);
        }

        public IEnumerable<Venue> Search(string city, string pitchType, int? maxPrice)
        {
            using var db = new LiteDatabase(_dbPath);
            var venues = db.GetCollection<Venue>("venues").FindAll();

            if (!string.IsNullOrWhiteSpace(city))
                venues = venues.Where(v => v.City.ToLower() == city.ToLower());

            if (!string.IsNullOrWhiteSpace(pitchType))
                venues = venues.Where(v => v.PitchType.ToLower() == pitchType.ToLower());

            if (maxPrice.HasValue)
                venues = venues.Where(v => v.Price <= maxPrice.Value);

            return venues.ToList();
        }
        public IEnumerable<Booking> GetAllBookings()
        {
            using var db = new LiteDatabase(_dbPath);
            return db.GetCollection<Booking>("bookings").FindAll().ToList();
        }

        public IEnumerable<ContactMessage> GetAllMessages()
        {
            using var db = new LiteDatabase(_dbPath);
            return db.GetCollection<ContactMessage>("messages").FindAll().ToList();
        }

        public void AddBooking(Booking booking)
        {
            using var db = new LiteDatabase(_dbPath);
            var col = db.GetCollection<Booking>("bookings");
            col.Insert(booking);
        }

        public void AddContact(ContactMessage msg)
        {
            using var db = new LiteDatabase(_dbPath);
            var col = db.GetCollection<ContactMessage>("messages");
            col.Insert(msg);
        }

        public IEnumerable<ContactMessage> GetContactMessages()
        {
            using var db = new LiteDatabase(_dbPath);
            return db.GetCollection<ContactMessage>("messages").FindAll().ToList();
        }
    }
}
