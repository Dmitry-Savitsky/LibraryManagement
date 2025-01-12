using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
using System;

namespace LibraryManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookCharacteristicsController : ControllerBase
    {
        private readonly BookCharacteristicsService _bookCharacteristicsService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookCharacteristicsController(BookCharacteristicsService bookCharacteristicsService, IWebHostEnvironment webHostEnvironment)
        {
            _bookCharacteristicsService = bookCharacteristicsService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookCharacteristics = await _bookCharacteristicsService.GetAllAsync();
            return Ok(bookCharacteristics);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bookCharacteristic = await _bookCharacteristicsService.GetByIdAsync(id);
            if (bookCharacteristic == null)
                return NotFound($"BookCharacteristics with ID {id} not found.");
            return Ok(bookCharacteristic);
        }

        [HttpGet("author/{authorId}")]
        public async Task<IActionResult> GetBooksByAuthorId(int authorId)
        {
            var books = await _bookCharacteristicsService.GetBooksByAuthorIdAsync(authorId);
            if (books == null)
                return NotFound($"No books found for author with ID {authorId}.");
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] BookCharacteristicsDto dto)
        {
            var imagePath = await SaveImageAsync(dto.Image);
            var bookCharacteristic = await _bookCharacteristicsService.AddAsync(dto, imagePath);
            return CreatedAtAction(nameof(GetById), new { id = bookCharacteristic.Id }, bookCharacteristic);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] BookCharacteristicsDto dto)
        {
            var imagePath = dto.Image != null ? await SaveImageAsync(dto.Image) : null;
            var isUpdated = await _bookCharacteristicsService.UpdateAsync(id, dto, imagePath);
            if (!isUpdated)
                return NotFound($"BookCharacteristics with ID {id} not found.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _bookCharacteristicsService.DeleteAsync(id);
            if (!isDeleted)
                return NotFound($"BookCharacteristics with ID {id} not found.");
            return NoContent();
        }

        private async Task<string> SaveImageAsync(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return string.Empty;

            var uniqueFileName = $"{Guid.NewGuid()}_{image.FileName}";
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return "/images/" + uniqueFileName;
        }
    }
}
