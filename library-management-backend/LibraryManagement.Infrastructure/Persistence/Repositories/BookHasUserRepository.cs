using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Infrastructure.Persistence;
using LibraryManagement.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookHasUserRepository : Repository<BookHasUser>, IBookHasUserRepository
    {
        private readonly LibraryDbContext _dbContext;

        public BookHasUserRepository(LibraryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Book>> GetAvailableBooksByCharacteristicsIdAsync(int bookCharacteristicsId)
        {
            var books = await _dbContext.Books
                .Where(b => b.BookCharacteristicsId == bookCharacteristicsId)
                .ToListAsync();

            var reservedBookIds = await _dbContext.BookHasUsers
                .Where(bhu => bhu.TimeReturned == null)
                .Select(bhu => bhu.BookId)
                .ToListAsync();

            return books.Where(b => !reservedBookIds.Contains(b.Id)).ToList();
        }

        public async Task<IEnumerable<BookHasUser>> GetByConditionAsync(Expression<Func<BookHasUser, bool>> predicate)
        {
            return await _dbContext.BookHasUsers.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetUserBooksAsync(int userId)
        {
            return await _dbContext.BookHasUsers
                .Where(bhu => bhu.UserId == userId)
                .Select(bhu => new
                {
                    BookId = bhu.BookId,
                    BookCharacteristics = bhu.Book.BookCharacteristics,
                    TimeBorrowed = bhu.TimeBorrowed
                })
                .Distinct()
                .ToListAsync();
        }


    }
}
