using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; 
        public string Name { get; set; } = string.Empty;
        public ICollection<BookHasUser> BorrowedBooks { get; set; } = new List<BookHasUser>();
    }
}
