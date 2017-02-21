namespace ContosoUniversityAngular.Features.Instructors
{
    using AutoMapper;
    using Data.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Instructor, Index.Response.Instructor>()
                .ForMember(i => i.Courses, opt => opt.MapFrom(i => i.CourseInstructors));

            CreateMap<CourseInstructor, Index.Response.Instructor.Course>()
                .ForMember(c => c.Id, opt => opt.MapFrom(c => c.CourseId))
                .ForMember(c => c.Title, opt => opt.MapFrom(c => c.Course.Title));

            CreateMap<CreateEdit.Command, Instructor>()
                .ForSourceMember(c => c.SelectedCourses, opt => opt.Ignore())
                .ForMember(i => i.CourseInstructors, opt => opt.Ignore());

            CreateMap<Instructor, CreateEdit.Response>();

            CreateMap<Instructor, Details.Response>()
                .ForMember(i => i.Courses, opt => opt.MapFrom(i => i.CourseInstructors));

            CreateMap<CourseInstructor, Details.Response.Course>()
                .ForMember(c => c.Id, opt => opt.MapFrom(c => c.CourseId))
                .ForMember(c => c.Title, opt => opt.MapFrom(c => c.Course.Title));
        }
    }
}
