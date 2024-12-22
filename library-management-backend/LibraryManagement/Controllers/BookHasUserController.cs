using LibraryManagement.Core.Interfaces;
using LibraryManagement.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookHasUserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookHasUserRepository _bookHasUserRepository;

        public BookHasUserController(IUnitOfWork unitOfWork, IBookHasUserRepository bookHasUserRepository)
        {
            _unitOfWork = unitOfWork;
            _bookHasUserRepository = bookHasUserRepository;
        }

        [HttpPost("reserve")]
        public async Task<IActionResult> ReserveBook(ReserveBookDto reserveBookDto)
        {
            var bookCharacteristics = await _unitOfWork.BookCharacteristics.GetByIdAsync(reserveBookDto.BookCharacteristicsId);

            if (bookCharacteristics == null)
                return NotFound("Book characteristics not found.");

            if (bookCharacteristics.BookCount <= 0)
                return BadRequest("No available copies of the book.");

            var availableBooks = await _bookHasUserRepository.GetAvailableBooksByCharacteristicsIdAsync(reserveBookDto.BookCharacteristicsId);

            if (!availableBooks.Any())
                return BadRequest("No available copies of this book.");

            var bookToReserve = availableBooks.First();

            var bookHasUser = new BookHasUser
            {
                BookId = bookToReserve.Id,
                UserId = reserveBookDto.UserId,
                TimeBorrowed = DateTime.UtcNow,
                TimeReturned = null
            };

            await _unitOfWork.BooksHasUsers.AddAsync(bookHasUser);

            bookCharacteristics.BookCount--;

            _unitOfWork.BookCharacteristics.Update(bookCharacteristics);

            await _unitOfWork.SaveChangesAsync();
            return Ok(new
            {
                Message = "Book reserved successfully.",
                BookId = bookToReserve.Id,
                UserId = reserveBookDto.UserId,
                TimeBorrowed = bookHasUser.TimeBorrowed
            });
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook(ReturnBookDto returnBookDto)
        {
            var bookHasUser = await _bookHasUserRepository
                .GetByConditionAsync(b => b.BookId == returnBookDto.BookId && b.UserId == returnBookDto.UserId);


            if (!bookHasUser.Any())
                return NotFound("No record found for this book and user.");

            var book = await _unitOfWork.Books.GetByIdAsync(returnBookDto.BookId);

            if (book == null)
                return NotFound("Book not found.");

            var bookCharacteristics = await _unitOfWork.BookCharacteristics
                .GetByIdAsync(book.BookCharacteristicsId);

            if (bookCharacteristics == null)
                return NotFound("Book characteristics not found.");

            var recordToRemove = bookHasUser.First();
            _unitOfWork.BooksHasUsers.Delete(recordToRemove);

            bookCharacteristics.BookCount += 1;

            _unitOfWork.BookCharacteristics.Update(bookCharacteristics);

            await _unitOfWork.SaveChangesAsync();

            return Ok(new
            {
                Message = "Book returned successfully.",
                BookId = returnBookDto.BookId,
                UserId = returnBookDto.UserId
            });
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserBooks(int userId)
        {
            var userBooks = await _bookHasUserRepository.GetUserBooksAsync(userId);

            if (!userBooks.Any())
                return NotFound("No books found for this user.");

            return Ok(userBooks);
        }



    }
}
