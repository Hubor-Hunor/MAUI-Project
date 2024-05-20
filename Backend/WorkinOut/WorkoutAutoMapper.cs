using AutoMapper;
using WorkinOut.Dtos;
using WorkinOut.Models;

namespace WorkinOut
{
    public class WorkoutAutoMapper : Profile
    {
        public WorkoutAutoMapper() 
        {
            CreateMap<WorkoutCreateRequest, Workout>();
        }
    }
}
