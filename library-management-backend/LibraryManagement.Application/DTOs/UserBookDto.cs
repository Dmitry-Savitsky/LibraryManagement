using LibraryManagement.Core.Entities;

namespace LibraryManagement.Application.DTOs
{
    public class UserBookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public DateTime TimeBorrowed { get; set; }
        public DateTime? TimeReturned { get; set; }
    }

}

