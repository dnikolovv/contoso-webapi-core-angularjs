namespace ContosoUniversityAngular.Features.Departments
{
    using AutoMapper;
    using Data.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, Index.Response.Department>();
            CreateMap<Create.Command, Department>(MemberList.Source);
            CreateMap<Department, Create.Response>(MemberList.Destination);
            CreateMap<Edit.Command, Department>(MemberList.Source);
            CreateMap<Department, Edit.Response>(MemberList.Destination);
            CreateMap<Department, Details.Response>(MemberList.Destination);
            CreateMap<Instructor, Details.Response.Instructor>(MemberList.Destination);
        }
    }
}
