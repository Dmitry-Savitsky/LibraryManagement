using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Entities;
using LibraryManagement.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IUnitOfWork unitOfWork, IAuthorRepository authorRepository)
        {
            _unitOfWork = unitOfWork;
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var authors = await _unitOfWork.Authors.GetByIdAsync(id);
            if (authors == null)
                return NotFound($"Author with ID {id} not found.");
            return Ok(authors);
        }

        [HttpGet("/country")]
        public async Task<IActionResult> GetAuthorsByCountryAsync(string country)
        {
            var authors = await _authorRepository.GetAuthorsByCountryAsync(country);
            return Ok(authors);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AuthorDto authorDto)
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

            return CreatedAtAction(nameof(GetAll), new { id = author.Id }, author);
        }

    }
}
