namespace ContosoUniversityAngular.Features.Instructors
{
    using AutoMapper;
    using Data;
    using FluentValidation;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Details
    {
        public class Query : IAsyncRequest<Response>
        {
            public int? Id { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(q => q.Id).NotNull();
            }
        }

        public class Response
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

        public class QueryHandler : IAsyncRequestHandler<Query, Response>
        {
            public QueryHandler(UniversityContext context)
            {
                _context = context;
            }

            private readonly UniversityContext _context;

            public async Task<Response> Handle(Query message)
            {
                var instructorInDb = await _context
                    .Instructors
                    .Include(i => i.CourseInstructors)
                        .ThenInclude(c => c.Course)
                    .FirstOrDefaultAsync(i => i.Id == message.Id);

                return Mapper.Map<Response>(instructorInDb);
            }
        }
    }
}
