using AutoMapper;
using WorkinOut.Dtos;
using WorkinOut.Models;

namespace WorkinOut
{
    public class PersonAutoMapper : Profile
    {
        public PersonAutoMapper() 
        {
            CreateMap<PersonCreateRequest, Person>();
        }
    }
}
