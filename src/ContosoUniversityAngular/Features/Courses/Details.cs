namespace ContosoUniversityAngular.Features.Courses
{
    using AutoMapper;
    using Data;
    using Data.Models;
    using FluentValidation;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
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

            public string Title { get; set; }

            public int Credits { get; set; }

            public DepartmentDto Department { get; set; }

            public ICollection<Enrollment> Enrollments { get; set; }

            public class DepartmentDto
            {
                public int Id { get; set; }

                public string Name { get; set; }
            }

            public class Enrollment
            {
                public int CourseId { get; set; }

                public int StudentId { get; set; }

                public StudentDto Student { get; set; }

                public CourseDto Course { get; set; }

                public string Grade { get; set; }

                public class StudentDto
                {
                    public int Id { get; set; }

                    public string FullName { get; set; }
                }

                public class CourseDto
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
                var course = await _context.Courses
                    .Include(c => c.Department)
                    .Include(c => c.Enrollments)
                        .ThenInclude(e => e.Student)
                    .Include(c => c.Enrollments)
                        .ThenInclude(e => e.Course)
                    .FirstOrDefaultAsync(c => c.Id == (int)message.Id);

                return Mapper.Map<Response>(course);
            }
        }
    }
}
