using LibraryManagement.Application.DTOs;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public class BookHasUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookHasUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> ReserveBookAsync(ReserveBookDto reserveBookDto)
        {
            var bookCharacteristics = await _unitOfWork.BookCharacteristics.GetByIdAsync(reserveBookDto.BookCharacteristicsId);

            if (bookCharacteristics == null)
                return "Book characteristics not found.";

            if (bookCharacteristics.BookCount <= 0)
                return "No available copies of the book.";

            var availableBooks = await _unitOfWork.BookHasUserRepository.GetAvailableBooksByCharacteristicsIdAsync(reserveBookDto.BookCharacteristicsId);

            if (!availableBooks.Any())
                return "No available copies of this book.";

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
            return "Book reserved successfully.";
        }

        public async Task<string> ReturnBookAsync(ReturnBookDto returnBookDto)
        {
            var bookHasUser = await _unitOfWork.BookHasUserRepository
                .GetByConditionAsync(b => 
                    b.BookId == returnBookDto.BookId && 
                    b.UserId == returnBookDto.UserId && 
                    b.TimeBorrowed == returnBookDto.TimeBorrowed);

            if (!bookHasUser.Any())
                return "No record found for this book and user.";

            var book = await _unitOfWork.Books.GetByIdAsync(returnBookDto.BookId);
            if (book == null)
                return "Book not found.";

            var bookCharacteristics = await _unitOfWork.BookCharacteristics
                .GetByIdAsync(book.BookCharacteristicsId);
            if (bookCharacteristics == null)
                return "Book characteristics not found.";

            var recordToUpdate = bookHasUser.First();
            recordToUpdate.TimeReturned = DateTime.UtcNow;

            _unitOfWork.BooksHasUsers.Update(recordToUpdate);

            bookCharacteristics.BookCount++;
            _unitOfWork.BookCharacteristics.Update(bookCharacteristics);

            await _unitOfWork.SaveChangesAsync();

            return "Book returned successfully.";
        }

        public async Task<IEnumerable<object>> GetUserBooksAsync(int userId)
        {
            return await _unitOfWork.BookHasUserRepository.GetUserBooksAsync(userId);
        }
    }
}
