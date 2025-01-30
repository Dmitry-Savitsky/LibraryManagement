using System;
using Microsoft.AspNetCore.Http;


namespace LibraryManagement.Application.DTOs
{
    public class BookCharacteristicsCreateDto
    {
        public int ISBN { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public IFormFile? Image { get; set; }
        public int CheckoutPeriod { get; set; }
        public int BookCount { get; set; }
    }

    public class BookCharacteristicsResponseDto
    {
        public int Id { get; set; }
        public int ISBN { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public string ImgPath { get; set; } = string.Empty;  // Для клиента
        public int CheckoutPeriod { get; set; }
        public int BookCount { get; set; }
    }
}
