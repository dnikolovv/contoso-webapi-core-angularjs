namespace ContosoUniversityAngular.Features.Courses
{
    using AutoMapper;
    using Data.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, Index.Response.Course>();
            CreateMap<Course, Create.Response>();
            CreateMap<Create.Command, Course>(MemberList.None);
            CreateMap<Course, Details.Response>(MemberList.Destination);
            CreateMap<Grade, string>().ConvertUsing(src => src.ToString());
            CreateMap<Course, Details.Response.Enrollment.CourseDto>(MemberList.Destination);
            CreateMap<Enrollment, Details.Response.Enrollment>(MemberList.Destination);
            CreateMap<Student, Details.Response.Enrollment.StudentDto>(MemberList.Destination);
            CreateMap<Department, Details.Response.DepartmentDto>(MemberList.Destination);
            CreateMap<Course, Edit.Command>();
            CreateMap<Edit.Command, Course>(MemberList.None);
            CreateMap<Course, Edit.Response>();
            CreateMap<Course, Delete.Command>();
        }
    }
}
