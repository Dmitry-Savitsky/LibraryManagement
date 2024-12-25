using LibraryManagement.Application.DTOs;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public class AuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IUnitOfWork unitOfWork, IAuthorRepository authorRepository)
        {
            _unitOfWork = unitOfWork;
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _unitOfWork.Authors.GetAllAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _unitOfWork.Authors.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Author>> GetAuthorsByCountryAsync(string country)
        {
            return await _authorRepository.GetAuthorsByCountryAsync(country);
        }

        public async Task<Author> AddAuthorAsync(AuthorDto authorDto)
        {
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
                return false;

            _unitOfWork.Authors.Delete(author);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}