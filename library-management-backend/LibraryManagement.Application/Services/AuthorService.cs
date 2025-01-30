using AutoMapper;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public class AuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author == null)
                throw new NotFoundException($"Author with ID {id} not found.");

            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<AuthorDto> AddAuthorAsync(AuthorDto authorDto)
        {
            var existingAuthor = await _unitOfWork.AuthorRepository.GetByConditionAsync(a => a.Name == authorDto.Name && a.Surename == authorDto.Surename);
            if (existingAuthor.Any())
                throw new AlreadyExistsException("Author with the same name and surname already exists.");

            var author = _mapper.Map<Author>(authorDto);
            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AuthorDto>(author);
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
