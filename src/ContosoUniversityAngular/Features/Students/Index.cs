namespace ContosoUniversityAngular.Features.Students
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
            public ICollection<Student> Students;

            public class Student
            {
                public int Id { get; set; }

                public string FullName { get; set; }

                public DateTime EnrollmentDate { get; set; }
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
                var students = await _context
                    .Students
                    .ProjectTo<Response.Student>()
                    .ToListAsync();

                return new Response
                {
                    Students = students
                };
            }
        }

    }
}
