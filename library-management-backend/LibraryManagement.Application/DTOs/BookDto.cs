using System.Collections.Generic;

namespace LibraryManagement.Application.DTOs
{
    public class BookDto
    {
        public int BookCharacteristicsId { get; set; }
        public BookCharacteristicsDto BookCharacteristics { get; set; } = null!;
    }
}
