namespace ContosoUniversityAngular.Features.Students
{
    using AutoMapper;
    using Data;
    using Data.Models;
    using FluentValidation;
    using MediatR;
    using System;
    using System.Threading.Tasks;

    public class Create
    {
        public class Command : IAsyncRequest<Response>
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public DateTime? EnrollmentDate { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.FirstName).NotEmpty().NotNull();
                RuleFor(c => c.LastName).NotEmpty().NotNull();
                RuleFor(c => c.EnrollmentDate).NotNull();
            }
        }

        public class Response
        {
            public int Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public DateTime EnrollmentDate { get; set; }
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
                var student = Mapper.Map<Student>(message);

                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                return Mapper.Map<Response>(student);
            }
        }
    }
}
