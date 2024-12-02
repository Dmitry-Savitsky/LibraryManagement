using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Persistence.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookCharacteristicsRepository : Repository<BookCharacteristics>, IBookCharacteristicsRepository
    {
        private readonly LibraryDbContext _dbContext;

        public BookCharacteristicsRepository(LibraryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public Task AddBookImageAsync(int bookCharacteristicsId, string imagePath)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BookCharacteristics>> GetBooksByAuthorIdAsync(int authorId)
        {
            return await _dbContext.BookCharacteristics
                .Where(bc => bc.AuthorId == authorId)
                .ToListAsync();
        }
    }
}
