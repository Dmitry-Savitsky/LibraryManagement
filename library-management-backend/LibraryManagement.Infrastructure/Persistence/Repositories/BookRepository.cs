using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Persistence.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly LibraryDbContext _dbContext;

        public BookRepository(LibraryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Book>> GetBookIdByBookCharacteristic(int id)
        {
            return await _dbContext.Books
                .Where(b => b.BookCharacteristicsId == id)
                .ToListAsync();
        }
    }
}
