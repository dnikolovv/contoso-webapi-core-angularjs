namespace ContosoUniversityAngular.Features.Departments
{
    using AutoMapper;
    using Data;
    using Data.Models;
    using FluentValidation;
    using MediatR;
    using System;

    public class Create
    {
        public class Command : IRequest<Response>
        {
            public string Name { get; set; }

            public decimal Budget { get; set; }

            public DateTime StartDate { get; set; }

            public Instructor Administrator { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
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

        public class CommandHandler : IRequestHandler<Command, Response>
        {
            public CommandHandler(UniversityContext context)
            {
                _context = context;
            }

            private readonly UniversityContext _context;

            public Response Handle(Command message)
            {
                var department = Mapper.Map<Department>(message);

                _context.Instructors.Attach(department.Administrator);
                _context.Departments.Add(department);
                _context.SaveChanges();

                return Mapper.Map<Response>(department);
            }
        }
    }
}
