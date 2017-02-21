namespace ContosoUniversityAngular.Features.Instructors
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Index
    {
        public class Query : IAsyncRequest<Response>
        {
        }

        public class Response
        {
            public ICollection<Instructor> Instructors { get; set; }

            public class Instructor
            {
                public int Id { get; set; }

                public string FirstName { get; set; }

                public string LastName { get; set; }

                public DateTime HireDate { get; set; }

                public ICollection<Course> Courses { get; set; }

                public class Course
                {
                    public int Id { get; set; }
                    public string Title { get; set; }
                }
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
                var instructors = await _context
                    .Instructors
                    .Include(i => i.CourseInstructors)
                        .ThenInclude(c => c.Course)
                    .ProjectTo<Response.Instructor>()
                    .ToListAsync();

                return new Response
                {
                    Instructors = instructors
                };
            }
        }
    }
}
