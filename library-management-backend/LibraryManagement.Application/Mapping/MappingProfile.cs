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
        CreateMap<BookHasUser, BookHasUserDto>()
            .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book ?? new Book()))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User ?? new User()))
            .ReverseMap();
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.BookCharacteristics, opt => opt.MapFrom(src => src.BookCharacteristics))
            .ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User"))
            .ForMember(dest => dest.Password, opt => opt.Ignore());
        CreateMap<ReturnBookDto, BookHasUser>();
        CreateMap<ReserveBookDto, BookHasUser>()
            .ForMember(dest => dest.BookId, opt => opt.Ignore())
            .ForMember(dest => dest.TimeBorrowed, opt => opt.Ignore())
            .ForMember(dest => dest.TimeReturned, opt => opt.Ignore());
    }
}
