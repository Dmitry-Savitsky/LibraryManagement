using LibraryManagement.Application.DTOs;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Exceptions;
using LibraryManagement.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public class BookCharacteristicsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookCharacteristicsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BookCharacteristics>> GetAllAsync()
        {
            return await _unitOfWork.BookCharacteristics.GetAllAsync();
        }

        public async Task<BookCharacteristics> GetByIdAsync(int id)
        {
            var bookCharacteristics = await _unitOfWork.BookCharacteristics.GetByIdAsync(id);
            if (bookCharacteristics == null)
                throw new NotFoundException($"Book characteristics with ID {id} not found.");

            return bookCharacteristics;
        }

        public async Task<IEnumerable<BookCharacteristics>> GetBooksByAuthorIdAsync(int authorId)
        {
            return await _unitOfWork.BookCharacteristicsRepository.GetBooksByAuthorIdAsync(authorId);
        }

        public async Task<BookCharacteristics> AddAsync(BookCharacteristicsDto dto, string imagePath)
        {
            // добавить GetByConditionAsync

            //var existingBook = await _unitOfWork.BookCharacteristics.GetByConditionAsync(b => b.ISBN == dto.ISBN);
            //if (existingBook != null)
            //    throw new AlreadyExistsException("Book with the same ISBN already exists.");

            var bookCharacteristic = new BookCharacteristics
            {
                ISBN = dto.ISBN,
                Title = dto.Title,
                Genre = dto.Genre,
                Description = dto.Description,
                AuthorId = dto.AuthorId,
                CheckoutPeriod = dto.CheckoutPeriod,
                BookCount = dto.BookCount,
                ImgPath = imagePath
            };

            await _unitOfWork.BookCharacteristics.AddAsync(bookCharacteristic);

            for (int i = 0; i < dto.BookCount; i++)
            {
                var book = new Book { BookCharacteristicsId = bookCharacteristic.Id };
                await _unitOfWork.Books.AddAsync(book);
            }

            await _unitOfWork.SaveChangesAsync();
            return bookCharacteristic;
        }

        public async Task<bool> UpdateAsync(int id, BookCharacteristicsDto dto, string imagePath)
        {
            var bookCharacteristic = await _unitOfWork.BookCharacteristics.GetByIdAsync(id);
            if (bookCharacteristic == null)
                throw new NotFoundException($"Book characteristics with ID {id} not found.");

            bookCharacteristic.ISBN = dto.ISBN;
            bookCharacteristic.Title = dto.Title;
            bookCharacteristic.Genre = dto.Genre;
            bookCharacteristic.Description = dto.Description;
            bookCharacteristic.AuthorId = dto.AuthorId;
            bookCharacteristic.CheckoutPeriod = dto.CheckoutPeriod;
            bookCharacteristic.BookCount = dto.BookCount;

            if (!string.IsNullOrEmpty(imagePath))
                bookCharacteristic.ImgPath = imagePath;

            _unitOfWork.BookCharacteristics.Update(bookCharacteristic);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var bookCharacteristic = await _unitOfWork.BookCharacteristics.GetByIdAsync(id);
            if (bookCharacteristic == null)
                throw new NotFoundException($"Book characteristics with ID {id} not found.");

            _unitOfWork.BookCharacteristics.Delete(bookCharacteristic);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}