using LibraryManagement.Application.DTOs;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public class BookCharacteristicsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookCharacteristicsRepository _bookCharacteristicsRepository;

        public BookCharacteristicsService(IUnitOfWork unitOfWork, IBookCharacteristicsRepository bookCharacteristicsRepository)
        {
            _unitOfWork = unitOfWork;
            _bookCharacteristicsRepository = bookCharacteristicsRepository;
        }

        public async Task<IEnumerable<BookCharacteristics>> GetAllAsync()
        {
            return await _unitOfWork.BookCharacteristics.GetAllAsync();
        }

        public async Task<BookCharacteristics> GetByIdAsync(int id)
        {
            return await _unitOfWork.BookCharacteristics.GetByIdAsync(id);
        }

        public async Task<IEnumerable<BookCharacteristics>> GetBooksByAuthorIdAsync(int authorId)
        {
            return await _bookCharacteristicsRepository.GetBooksByAuthorIdAsync(authorId);
        }

        public async Task<BookCharacteristics> AddAsync(BookCharacteristicsDto dto, string imagePath)
        {
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
                return false;

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
                return false;

            _unitOfWork.BookCharacteristics.Delete(bookCharacteristic);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
