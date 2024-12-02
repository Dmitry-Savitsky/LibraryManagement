using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public int BookCharacteristicsId { get; set; }
        public BookCharacteristics BookCharacteristics { get; set; } = null!;
        public ICollection<BookHasUser> BorrowedBy { get; set; } = new List<BookHasUser>();
    }
}
