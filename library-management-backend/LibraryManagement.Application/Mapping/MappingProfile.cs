using AutoMapper;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Core.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Author, AuthorDto>().ReverseMap();
        CreateMap<BookCharacteristics, BookCharacteristicsCreateDto>().ReverseMap();
        CreateMap<BookCharacteristics, BookCharacteristicsResponseDto>().ReverseMap();
        CreateMap<Book, BookDto>().ReverseMap();
        CreateMap<BookHasUser, BookHasUserDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User"))
            .ForMember(dest => dest.Password, opt => opt.Ignore());
        CreateMap<ReturnBookDto, BookHasUser>();
    }
}
