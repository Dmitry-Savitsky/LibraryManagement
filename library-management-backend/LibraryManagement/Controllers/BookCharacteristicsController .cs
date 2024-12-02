using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace LibraryManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookCharacteristicsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookCharacteristicsRepository _bookCharacteristicsRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookCharacteristicsController(IUnitOfWork unitOfWork, IBookCharacteristicsRepository bookCharacteristicsRepository, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _bookCharacteristicsRepository = bookCharacteristicsRepository;
            _webHostEnvironment = webHostEnvironment;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookCharacteristics = await _unitOfWork.BookCharacteristics.GetAllAsync();
            return Ok(bookCharacteristics);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bookCharacteristic = await _unitOfWork.BookCharacteristics.GetByIdAsync(id);
            if (bookCharacteristic == null)
                return NotFound($"BookCharacteristics with ID {id} not found.");

            return Ok(bookCharacteristic);
        }

        [HttpGet("author/{authorId}")]
        public async Task<IActionResult> GetBooksByAuthorId(int authorId)
        {
            var books = await _bookCharacteristicsRepository.GetBooksByAuthorIdAsync(authorId);

            if (!books.Any())
            {
                return NotFound($"No books found for author with ID {authorId}.");
            }

            return Ok(books);
        }


        [HttpPost]
        public async Task<IActionResult> Add(BookCharacteristicsDto dto)
        {
            var bookCharacteristic = new BookCharacteristics
            {
                ISBN = dto.ISBN,
                Title = dto.Title,
                Genre = dto.Genre,
                Description = dto.Description,
                AuthorId = dto.AuthorId,
                CheckoutPeriod = dto.CheckoutPeriod,
                BookCount = dto.BookCount
            };

            var imagePath = await SaveImageAsync(dto.Image);
            bookCharacteristic.ImgPath = imagePath;

            await _unitOfWork.BookCharacteristics.AddAsync(bookCharacteristic);
            await _unitOfWork.SaveChangesAsync();

            for (int i = 0; i < dto.BookCount; i++)
            {
                var book = new Book
                {
                    BookCharacteristicsId = bookCharacteristic.Id
                };
                await _unitOfWork.Books.AddAsync(book);
            }

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = bookCharacteristic.Id }, bookCharacteristic);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookCharacteristicsDto dto)
        {
            var bookCharacteristic = await _unitOfWork.BookCharacteristics.GetByIdAsync(id);
            if (bookCharacteristic == null)
                return NotFound($"BookCharacteristics with ID {id} not found.");

            bookCharacteristic.ISBN = dto.ISBN;
            bookCharacteristic.Title = dto.Title;
            bookCharacteristic.Genre = dto.Genre;
            bookCharacteristic.Description = dto.Description;
            bookCharacteristic.AuthorId = dto.AuthorId;
            bookCharacteristic.CheckoutPeriod = dto.CheckoutPeriod;
            bookCharacteristic.BookCount = dto.BookCount;
            bookCharacteristic.ImgPath = bookCharacteristic.ImgPath;

            if (dto.Image != null && dto.Image.Length > 0)
            {
                var imagePath = await SaveImageAsync(dto.Image);
                bookCharacteristic.ImgPath = imagePath;
            }

            _unitOfWork.BookCharacteristics.Update(bookCharacteristic);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bookCharacteristic = await _unitOfWork.BookCharacteristics.GetByIdAsync(id);
            if (bookCharacteristic == null)
                return NotFound($"BookCharacteristics with ID {id} not found.");

            _unitOfWork.BookCharacteristics.Delete(bookCharacteristic);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
