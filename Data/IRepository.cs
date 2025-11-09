using BoxCricketBuddy.Models;
using System.Collections.Generic;

namespace BoxCricketBuddy.Data
{
    public interface IRepository
    {
        IEnumerable<Venue> GetAllVenues();
        Venue? GetVenue(int id);
        IEnumerable<Venue> Search(string city, string? pitchType, int? maxPrice);
        void AddBooking(Booking booking);
        void AddContact(ContactMessage msg);
        IEnumerable<ContactMessage> GetContactMessages();
    }
}
