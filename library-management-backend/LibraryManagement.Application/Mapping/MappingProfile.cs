using AutoMapper;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Core.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Author mapping
        CreateMap<Author, AuthorDto>().ReverseMap();

        // BookCharacteristics mapping
        CreateMap<BookCharacteristics, BookCharacteristicsDto>()
            .ForMember(dest => dest.Image, opt => opt.Ignore()); // Игнорируем IFormFile, так как это не сущность

        CreateMap<BookCharacteristicsDto, BookCharacteristics>()
            .ForMember(dest => dest.ImgPath, opt => opt.Ignore()); // ImagePath будет задаваться отдельно

        // Book mapping
        CreateMap<Book, BookDto>().ReverseMap();

        // BookHasUser mapping
        CreateMap<BookHasUser, BookHasUserDto>().ReverseMap();

        // User mapping
        CreateMap<User, UserDto>().ReverseMap();
    }
}
