using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
                return NotFound($"Author with ID {id} not found.");
            return Ok(author);
        }

        //[HttpGet("/country")]
        //public async Task<IActionResult> GetAuthorsByCountryAsync(string country)
        //{
        //    var authors = await _authorService.GetAuthorsByCountryAsync(country);
        //    return Ok(authors);
        //}

        [HttpPost]
        public async Task<IActionResult> Add(AuthorDto authorDto)
        {
            var author = await _authorService.AddAuthorAsync(authorDto);
            return CreatedAtAction(nameof(GetAll), new { id = author.Id }, author);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _authorService.DeleteAuthorAsync(id);
            if (!isDeleted)
                return NotFound($"Author with ID {id} not found.");

            return NoContent();
        }
    }
}
