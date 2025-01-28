using LibraryManagement.Core.Entities;
using System.Linq.Expressions;

namespace LibraryManagement.Core.Interfaces
{
    public interface IBookCharacteristicsRepository : IRepository<BookCharacteristics>
    {

        Task<IEnumerable<BookCharacteristics>> GetBooksByAuthorIdAsync(int authorId);

        Task<IEnumerable<BookCharacteristics>> GetByConditionAsync(Expression<Func<BookCharacteristics, bool>> predicate);

    }
}