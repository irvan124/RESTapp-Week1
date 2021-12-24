using AutoMapper;

namespace RESTapp.Profiles
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {

            CreateMap<Models.Course, Dtos.CourseDto>();
            CreateMap<Dtos.CourseModifyDto, Models.Course>();
        }
    }
}
