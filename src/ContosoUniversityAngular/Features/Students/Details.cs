namespace ContosoUniversityAngular.Features.Students
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

            public string FullName { get; set; }

            public DateTime EnrollmentDate { get; set; }

            public ICollection<Enrollment> Enrollments { get; set; }

            public class Enrollment
            {
                public int CourseId { get; set; }

                public int StudentId { get; set; }

                public CourseDto Course { get; set; }

                public StudentDto Student { get; set; }

                public class CourseDto
                {
                    public int Id { get; set; }

                    public string Title { get; set; }
                }

                public class StudentDto
                {
                    public int Id { get; set; }

                    public string FullName { get; set; }
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
                var studentInDb = await _context
                    .Students
                    .Include(s => s.Enrollments)
                        .ThenInclude(e => e.Course)
                    .Include(s => s.Enrollments)
                        .ThenInclude(e => e.Student)        
                    .FirstOrDefaultAsync(s => s.Id == message.Id);

                return Mapper.Map<Response>(studentInDb);
            }
        }
    }
}
