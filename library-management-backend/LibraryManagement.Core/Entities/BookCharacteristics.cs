using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Entities
{
    public class BookCharacteristics
    {
        public int Id { get; set; }
        public int ISBN { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
        public string ImgPath { get; set; } = string.Empty;
        public int CheckoutPeriod { get; set; }
        public int BookCount { get; set; }
    }
}
