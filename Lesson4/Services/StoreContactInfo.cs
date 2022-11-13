using Lesson4.Models;

namespace Lesson4.Services
{
    public class StoreContactInfo
    {
        public IList<Contact> Contacts { get; set; } = new List<Contact>();
    }
}