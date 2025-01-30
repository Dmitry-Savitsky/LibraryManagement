using AutoMapper;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Exceptions;
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
        private readonly IMapper _mapper;

        public BookHasUserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string> ReserveBookAsync(ReserveBookDto reserveBookDto)
        {
            var bookCharacteristics = await _unitOfWork.BookCharacteristics.GetByIdAsync(reserveBookDto.BookCharacteristicsId);
            if (bookCharacteristics == null)
                throw new NotFoundException("Book characteristics not found.");

            if (bookCharacteristics.BookCount <= 0)
                throw new BadRequestException("No available copies of the book.");

            var availableBooks = await _unitOfWork.BookHasUserRepository.GetAvailableBooksByCharacteristicsIdAsync(reserveBookDto.BookCharacteristicsId);
            if (!availableBooks.Any())
                throw new BadRequestException("No available copies of this book.");

            var bookToReserve = availableBooks.First();
            var bookHasUser = _mapper.Map<BookHasUser>(reserveBookDto);
            bookHasUser.BookId = bookToReserve.Id;
            bookHasUser.TimeBorrowed = DateTime.UtcNow;
            bookHasUser.TimeReturned = null;

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
                throw new NotFoundException("No record found for this book and user.");

            var book = await _unitOfWork.Books.GetByIdAsync(returnBookDto.BookId);
            if (book == null)
                throw new NotFoundException("Book not found.");

            var bookCharacteristics = await _unitOfWork.BookCharacteristics
                .GetByIdAsync(book.BookCharacteristicsId);
            if (bookCharacteristics == null)
                throw new NotFoundException("Book characteristics not found.");

            var recordToUpdate = bookHasUser.First();
            recordToUpdate.TimeReturned = DateTime.UtcNow;

            _unitOfWork.BooksHasUsers.Update(recordToUpdate);

            bookCharacteristics.BookCount++;
            _unitOfWork.BookCharacteristics.Update(bookCharacteristics);

            await _unitOfWork.SaveChangesAsync();

            return "Book returned successfully.";
        }

        public async Task<IEnumerable<BookHasUserDto>> GetUserBooksAsync(int userId)
        {
            var userBooks = await _unitOfWork.BookHasUserRepository.GetUserBooksAsync(userId);
            return _mapper.Map<IEnumerable<BookHasUserDto>>(userBooks);
        }
    }
}
