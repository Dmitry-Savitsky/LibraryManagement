using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surename { get; set; } = string.Empty;
        public DateTime? Birthdate { get; set; }
        public string Country { get; set; } = string.Empty;
        public ICollection<BookCharacteristics> Books { get; set; } = new List<BookCharacteristics>();
    }
}
