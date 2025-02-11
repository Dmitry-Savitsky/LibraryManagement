using AutoMapper;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Exceptions;
using LibraryManagement.Core.Interfaces;

public class BookCharacteristicsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BookCharacteristicsService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookCharacteristicsResponseDto>> GetAllAsync()
    {
        var books = await _unitOfWork.BookCharacteristics.GetAllAsync();
        return _mapper.Map<IEnumerable<BookCharacteristicsResponseDto>>(books);
    }

    public async Task<BookCharacteristicsResponseDto> GetByIdAsync(int id)
    {
        var bookCharacteristics = await _unitOfWork.BookCharacteristics.GetByIdAsync(id);
        if (bookCharacteristics == null)
            throw new NotFoundException($"Book characteristics with ID {id} not found.");

        return _mapper.Map<BookCharacteristicsResponseDto>(bookCharacteristics);
    }

    public async Task<IEnumerable<BookCharacteristicsResponseDto>> GetBooksByAuthorIdAsync(int authorId)
    {
        var books = await _unitOfWork.BookCharacteristicsRepository.GetBooksByAuthorIdAsync(authorId);
        return _mapper.Map<IEnumerable<BookCharacteristicsResponseDto>>(books);
    }

    public async Task<(IEnumerable<BookCharacteristicsResponseDto>, int)> GetPaginatedAsync(int pageNumber, int pageSize)
    {
        var (books, totalItems) = await _unitOfWork.BookCharacteristicsRepository.GetPaginatedAsync(pageNumber, pageSize);
        return (_mapper.Map<IEnumerable<BookCharacteristicsResponseDto>>(books), totalItems);
    }

    public async Task<BookCharacteristicsResponseDto> AddAsync(BookCharacteristicsCreateDto dto, string imagePath)
    {
        var existingBook = await _unitOfWork.BookCharacteristicsRepository.GetByConditionAsync(b => b.ISBN == dto.ISBN);
        if (existingBook.Any())
            throw new AlreadyExistsException("Book with the same ISBN already exists.");

        var bookCharacteristic = _mapper.Map<BookCharacteristics>(dto);
        bookCharacteristic.ImgPath = imagePath;

        await _unitOfWork.BookCharacteristics.AddAsync(bookCharacteristic);
        await _unitOfWork.SaveChangesAsync();

        for (int i = 0; i < dto.BookCount; i++)
        {
            var book = new Book { BookCharacteristicsId = bookCharacteristic.Id };
            await _unitOfWork.Books.AddAsync(book);
        }

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<BookCharacteristicsResponseDto>(bookCharacteristic);
    }

    public async Task<bool> UpdateAsync(int id, BookCharacteristicsCreateDto dto, string imagePath)
    {
        var bookCharacteristic = await _unitOfWork.BookCharacteristics.GetByIdAsync(id);
        if (bookCharacteristic == null)
            throw new NotFoundException($"Book characteristics with ID {id} not found.");

        _mapper.Map(dto, bookCharacteristic);
        if (!string.IsNullOrEmpty(imagePath))
            bookCharacteristic.ImgPath = imagePath;

        _unitOfWork.BookCharacteristics.Update(bookCharacteristic);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var bookCharacteristic = await _unitOfWork.BookCharacteristics.GetByIdAsync(id);
        if (bookCharacteristic == null)
            throw new NotFoundException($"Book characteristics with ID {id} not found.");

        _unitOfWork.BookCharacteristics.Delete(bookCharacteristic);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
