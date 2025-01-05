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

        public IRepository<Author> Authors { get; }

        public IRepository<BookCharacteristics> BookCharacteristics { get; }

        public IRepository<Book> Books { get; }

        public IRepository<User> Users { get; }

        public IRepository<BookHasUser> BooksHasUsers { get; }

        // кастомные репозитории
        public IBookHasUserRepository BookHasUserRepository { get; }

        public IBookRepository BookRepository { get; }

        public IAuthorRepository AuthorRepository { get; }

        public IBookCharacteristicsRepository BookCharacteristicsRepository { get; }

        public IUserRepository UserRepository { get; }

        public UnitOfWork(LibraryDbContext context,
                  
                  IRepository<Author> authors,
                  IAuthorRepository authorRepository,

                  IRepository<BookCharacteristics> bookCharacteristics,
                  IBookCharacteristicsRepository bookCharacteristicsRepository,

                  IRepository<BookHasUser> booksHasUsers,
                  IBookHasUserRepository bookHasUserRepository,

                  IRepository<Book> books,
                  IBookRepository bookRepository,
                                    
                  IRepository<User> users,
                  IUserRepository userRepository)
        {
            _context = context;
            
            Authors = authors;
            AuthorRepository = authorRepository;

            BookCharacteristics = bookCharacteristics;
            BookCharacteristicsRepository = bookCharacteristicsRepository;

            BooksHasUsers = booksHasUsers;
            BookHasUserRepository = bookHasUserRepository;

            Books = books;
            BookRepository  = bookRepository;

            Users = users;
            UserRepository = userRepository;
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
