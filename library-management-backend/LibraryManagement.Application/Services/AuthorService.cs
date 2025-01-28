using LibraryManagement.Application.DTOs;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public class AuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _unitOfWork.Authors.GetAllAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author == null)
                throw new NotFoundException($"Author with ID {id} not found.");

            return author;
        }

        public async Task<Author> AddAuthorAsync(AuthorDto authorDto)
        {
            var existingAuthor = await _unitOfWork.Authors.GetByConditionAsync(a => a.Name == authorDto.Name && a.Surename == authorDto.Surename);
            if (existingAuthor.Any())
                throw new AlreadyExistsException("Author with the same name and surname already exists.");

            var author = new Author
            {
                Name = authorDto.Name,
                Surename = authorDto.Surename,
                Birthdate = authorDto.Birthdate,
                Country = authorDto.Country
            };

            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.SaveChangesAsync();

            return author;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author == null)
                throw new NotFoundException($"Author with ID {id} not found.");

            _unitOfWork.Authors.Delete(author);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}