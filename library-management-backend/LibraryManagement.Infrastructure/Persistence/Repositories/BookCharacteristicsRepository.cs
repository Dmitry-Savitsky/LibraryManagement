using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Persistence.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookCharacteristicsRepository : Repository<BookCharacteristics>, IBookCharacteristicsRepository
    {
        private readonly LibraryDbContext _dbContext;

        public BookCharacteristicsRepository(LibraryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BookCharacteristics>> GetBooksByAuthorIdAsync(int authorId)
        {
            return await _dbContext.BookCharacteristics
                .AsNoTracking()
                .Where(bc => bc.AuthorId == authorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<BookCharacteristics>> GetByConditionAsync(Expression<Func<BookCharacteristics, bool>> predicate)
        {
            return await _dbContext.BookCharacteristics
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }
    }
}
