using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces
{
    public interface IBookCharacteristicsRepository : IRepository<BookCharacteristics>
    {

        Task<IEnumerable<BookCharacteristics>> GetBooksByAuthorIdAsync(int authorId);

        Task<IEnumerable<BookCharacteristics>> GetByConditionAsync(Func<BookCharacteristics, bool> predicate);

    }
}