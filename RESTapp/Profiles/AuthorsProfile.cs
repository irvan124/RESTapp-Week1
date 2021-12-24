using AutoMapper;
using System;

namespace RESTapp.Profiles
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            CreateMap<Models.Author, Dtos.AuthorDto>()
             .ForMember(dest => dest.Name,
             opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Age,
             opt => opt.MapFrom(src => DateTime.Today.Year - src.DateOfBirth.Year));

            CreateMap<Dtos.AuthorModifyDto, Models.Author>();
        }

    }
}
