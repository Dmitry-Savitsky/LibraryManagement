﻿using System;
using Microsoft.AspNetCore.Http;


namespace LibraryManagement.Application.DTOs
{
    public class BookCharacteristicsDto
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
}
