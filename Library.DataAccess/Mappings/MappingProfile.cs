using AutoMapper;
using Library.Domain.Entities;
using Library.Shared.DTOs;

namespace Library.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookWithId>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<BookWithId, Book>();
        }
    }
}
