using LibraryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Author> Authors { get; }
        IRepository<BookCharacteristics> BookCharacteristics { get; }
        IRepository<Book> Books { get; }
        IRepository<User> Users { get; }
        IRepository<BookHasUser> BooksHasUsers { get; }

        IBookHasUserRepository BookHasUserRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
