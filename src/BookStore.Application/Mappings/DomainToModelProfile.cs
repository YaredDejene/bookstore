using AutoMapper;
using BookStore.Application.Models;
using BookStore.Domain.Models;

namespace BookStore.Application.Mappings
{
    public class DomainToModelProfile : Profile
    {
        public DomainToModelProfile()
        {
             CreateMap<Book, BookModel>();
        }
    }
}