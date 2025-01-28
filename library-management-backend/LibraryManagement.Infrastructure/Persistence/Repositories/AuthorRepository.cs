using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Persistence.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        private readonly LibraryDbContext _dbContext;

        public AuthorRepository(LibraryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Author>> GetAuthorsByCountryAsync(string country)
        {
            return await _dbContext.Authors
                .AsNoTracking()
                .Where(a => a.Country == country)
                .ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetByConditionAsync(Expression<Func<Author, bool>> predicate)
        {
            return await _dbContext.Authors
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }
    }
}
