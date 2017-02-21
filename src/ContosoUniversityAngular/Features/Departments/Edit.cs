namespace ContosoUniversityAngular.Features.Departments
{
    using AutoMapper;
    using Data;
    using Data.Models;
    using FluentValidation;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;

    public class Edit
    {
        public class Command : IAsyncRequest<Response>
        {
            public int? Id { get; set; }

            public string Name { get; set; }

            public decimal Budget { get; set; }

            public DateTime StartDate { get; set; }

            public Instructor Administrator { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.Id).NotNull();
                RuleFor(c => c.Name).NotEmpty();
                RuleFor(c => c.Budget).GreaterThan(0);
                RuleFor(c => c.Administrator).NotNull();
            }
        }

        public class Response
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public decimal Budget { get; set; }

            public DateTime StartDate { get; set; }

            public string AdministratorFullName { get; set; }
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
                var departmentInDb = await _context
                    .Departments
                    .FirstOrDefaultAsync(dep => dep.Id == message.Id);

                _context.Instructors.Attach(message.Administrator);
                Mapper.Map(message, departmentInDb);
                await _context.SaveChangesAsync();

                return Mapper.Map<Response>(departmentInDb);
            }
        }
    }
}
