using LibraryManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Application.Services
{
    public class AuthorService
    {
        //private readonly IUnitOfWork _unitOfWork;

        //public AuthorService(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        //public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        //{
        //    var authors = await _unitOfWork.Authors.GetAllAsync();
        //    return authors.Select(a => new AuthorDto
        //    {
        //        Id = a.Id,
        //        Name = a.Name,
        //        Surename = a.Surename,
        //        Birthdate = a.Birthdate,
        //        Country = a.Country
        //    });
        //}

        //public async Task AddAuthorAsync(AuthorDto authorDto)
        //{
        //    var author = new Author
        //    {
        //        Name = authorDto.Name,
        //        Surename = authorDto.Surename,
        //        Birthdate = authorDto.Birthdate,
        //        Country = authorDto.Country
        //    };
        //    await _unitOfWork.Authors.AddAsync(author);
        //    await _unitOfWork.SaveChangesAsync();
        //}
    }
}
