using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Entities
{
    public class BookHasUser
    {
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public DateTime TimeBorrowed { get; set; }
        public DateTime? TimeReturned { get; set; }
    }
}
