using LibraryManagement.Core.Entities;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book> GetByIsbnAsync(int isbn);
    }
}
