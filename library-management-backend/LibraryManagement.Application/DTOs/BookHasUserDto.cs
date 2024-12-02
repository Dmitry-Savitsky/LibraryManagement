using System;

namespace LibraryManagement.Application.DTOs
{
    public class BookHasUserDto
    {
        public int BookId { get; set; }
        public BookDto Book { get; set; } = null!;
        public int UserId { get; set; }
        public UserDto User { get; set; } = null!;
        public DateTime TimeBorrowed { get; set; }
    }
}
