using AutoMapper;
using BookStore.Application.Models;
using BookStore.Domain.Commands;

namespace BookStore.Application.Mappings
{
    public class ModelToDomainProfile : Profile
    {
        public ModelToDomainProfile()
        {
            CreateMap<BookModel, RegisterNewBookCommand>()
                .ConstructUsing(b => new RegisterNewBookCommand(b.Title, b.Description, b.PublishDate, b.Authors));
            CreateMap<BookModel, UpdateBookCommand>()
                .ConstructUsing(b => new UpdateBookCommand(b.Id, b.Title, b.Description, b.PublishDate, b.Authors));

        }
    }
}