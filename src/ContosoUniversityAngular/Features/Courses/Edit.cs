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

    public class Edit
    {
        public class Command : IAsyncRequest<Response>
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public int? Credits { get; set; }

            public Department Department { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator(ICoursesValidator validator)
            {
                RuleFor(c => c.Title).NotNull().Length(3, 50);
                RuleFor(c => c.Credits).NotNull().InclusiveBetween(0, 5);
                RuleFor(c => c.Department).NotNull();
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

                var course = await _context.Courses
                    .FirstOrDefaultAsync(c => c.Id == message.Id);

                Mapper.Map(message, course);

                await _context.SaveChangesAsync();

                return Mapper.Map<Response>(course);
            }
        }
    }
}
