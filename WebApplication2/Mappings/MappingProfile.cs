namespace WebApplication2.Mappings;

using AutoMapper;
using WebApplication2.Enteties;
using WebApplication2.DTO;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<UserRegisterDto, User>();
        CreateMap<Room, RoomDto>().ReverseMap();
        CreateMap<Booking, BookingDto>().ReverseMap();
        CreateMap<Review, ReviewDto>().ReverseMap();
    }
}
