using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces
{
    public interface IBookCharacteristicsRepository : IRepository<BookCharacteristics>
    {

        Task<IEnumerable<BookCharacteristics>> GetBooksByAuthorIdAsync(int authorId);

        Task AddBookImageAsync(int bookCharacteristicsId, string imagePath);
    }
}
