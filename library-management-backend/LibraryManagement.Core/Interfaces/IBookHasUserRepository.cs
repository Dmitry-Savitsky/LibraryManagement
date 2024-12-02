using LibraryManagement.Core.Entities;
using System.Linq.Expressions;

namespace LibraryManagement.Core.Interfaces
{
    public interface IBookHasUserRepository : IRepository<BookHasUser>
    {
        Task<IEnumerable<Book>> GetAvailableBooksByCharacteristicsIdAsync(int bookCharacteristicsId);
        Task<IEnumerable<BookHasUser>> GetByConditionAsync(Expression<Func<BookHasUser, bool>> predicate);
        Task<IEnumerable<object>> GetUserBooksAsync(int userId);
    }
}
