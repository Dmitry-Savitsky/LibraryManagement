using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookHasUserController : ControllerBase
    {
        private readonly BookHasUserService _bookHasUserService;

        public BookHasUserController(BookHasUserService bookHasUserService)
        {
            _bookHasUserService = bookHasUserService;
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPost("reserve")]
        public async Task<IActionResult> ReserveBook([FromBody] ReserveBookDto reserveBookDto)
        {
            var result = await _bookHasUserService.ReserveBookAsync(reserveBookDto);

            if (result == "Book characteristics not found." || result == "No available copies of the book." || result == "No available copies of this book.")
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookDto returnBookDto)
        {
            var result = await _bookHasUserService.ReturnBookAsync(returnBookDto);

            if (result == "No record found for this book and user." || result == "Book not found." || result == "Book characteristics not found.")
                return NotFound(result);

            return Ok(result);
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserBooks(int userId)
        {
            var userBooks = await _bookHasUserService.GetUserBooksAsync(userId);

            if (userBooks == null || !userBooks.Any())
                return NotFound("No books found for this user.");

            return Ok(userBooks);
        }
    }
}
