namespace ContosoUniversityAngular.Features.Courses
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Index
    {
        public class Query : IAsyncRequest<Response>
        {
            public string SelectedDepartmentName { get; set; }
        }

        public class Response
        {
            public string SelectedDepartmentName { get; set; }

            public ICollection<Course> Courses { get; set; }

            public class Course
            {
                public int Id { get; set; }
                
                public string Title { get; set; }
                
                public int Credits { get; set; }

                public string DepartmentName { get; set; }
            }
        }

        public class QueryHandler : IAsyncRequestHandler<Query, Response>
        {
            public QueryHandler(UniversityContext context)
            {
                _context = context;
            }

            private readonly UniversityContext _context;

            public async Task<Response> Handle(Query message)
            {
                var coursesQueryable = _context.Courses
                    .OrderBy(c => c.Id)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(message.SelectedDepartmentName))
                {
                    // Filter the courses if department name is specified
                    coursesQueryable = coursesQueryable
                        .Where(c => c.Department.Name == message.SelectedDepartmentName);
                }

                var courses = await coursesQueryable
                    .ProjectTo<Response.Course>()
                    .ToListAsync();

                return new Response
                {
                    SelectedDepartmentName = message.SelectedDepartmentName,
                    Courses = courses
                };
            }
        }
    }
}
