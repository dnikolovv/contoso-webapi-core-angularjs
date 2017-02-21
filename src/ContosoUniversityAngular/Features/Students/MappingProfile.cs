namespace ContosoUniversityAngular.Features.Students
{
    using AutoMapper;
    using Data.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, Index.Response.Student>(MemberList.Destination);
            CreateMap<Create.Command, Student>(MemberList.Source);
            CreateMap<Student, Create.Response>(MemberList.Destination);
            CreateMap<Edit.Command, Student>(MemberList.Source)
                .ForSourceMember(c => c.Enrollments, opt => opt.Ignore());
            CreateMap<Student, Edit.Response>(MemberList.Destination);
            CreateMap<Student, Details.Response>(MemberList.Destination);
            CreateMap<Enrollment, Details.Response.Enrollment>(MemberList.Destination);
            CreateMap<Student, Details.Response.Enrollment.StudentDto>(MemberList.Destination);
            CreateMap<Course, Details.Response.Enrollment.CourseDto>(MemberList.Destination);
        }
    }
}
