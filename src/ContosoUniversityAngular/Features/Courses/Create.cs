namespace ContosoUniversityAngular.Features.Courses
{
    using AutoMapper;
    using Data;
    using Data.Models;
    using FluentValidation;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using Validator;

    public class Create
    {
        public class Command : IAsyncRequest<Response>
        {
            public string Title { get; set; }

            public int Credits { get; set; }

            public Department Department { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator(ICoursesValidator validator)
            {
                RuleFor(c => c.Title).NotEmpty().NotNull();
                RuleFor(c => c.Credits).InclusiveBetween(1, 5);
                RuleFor(c => c.Department.Name).NotEmpty().NotNull();
                RuleFor(c => c.Department.Name)
                    .Must(name => validator.DepartmentExistsInDb(name))
                    .WithMessage("The department must be present in the database.");
            }
        }

        public class Response
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public int Credits { get; set; }

            public string DepartmentName { get; set; }
        }

        public class CommandHandler : IAsyncRequestHandler<Command, Response>
        {
            public CommandHandler(UniversityContext context)
            {
                _context = context;
            }

            private readonly UniversityContext _context;

            public async Task<Response> Handle(Command message)
            {
                _context.Departments.Attach(message.Department);

                Course course = Mapper.Map<Course>(message);

                _context.Courses.Add(course);
                await _context.SaveChangesAsync();

                return Mapper.Map<Response>(course);
            }
        }
    }
}
