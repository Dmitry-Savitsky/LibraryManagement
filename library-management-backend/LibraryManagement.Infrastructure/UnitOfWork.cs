using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Infrastructure.Persistence;
using LibraryManagement.Infrastructure.Persistence.Repositories;
using LibraryManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManagement.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;


        //private AuthorRepository _authorRepository;
        //public IAuthorRepository Authors => _authorRepository ??= new AuthorRepository(_context);


        public IRepository<Author> Authors { get; }

        public IRepository<BookCharacteristics> BookCharacteristics { get; }

        public IRepository<Book> Books { get; }

        public IRepository<User> Users { get; }

        public IRepository<BookHasUser> BooksHasUsers { get; }

        public UnitOfWork(LibraryDbContext context)
        {
            _context = context;
            Authors = new Repository<Author>(context);
            BookCharacteristics = new Repository<BookCharacteristics>(context);
            Books = new Repository<Book>(context);
            Users = new Repository<User>(context);
            BooksHasUsers = new Repository<BookHasUser>(context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
