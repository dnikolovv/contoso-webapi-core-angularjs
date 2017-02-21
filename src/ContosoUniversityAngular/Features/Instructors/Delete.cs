namespace ContosoUniversityAngular.Features.Instructors
{
    using Data;
    using FluentValidation;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class Delete
    {
        public class Command : IAsyncRequest
        {
            public int? Id { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.Id).NotNull();
            }
        }

        public class CommandHandler : AsyncRequestHandler<Command>
        {
            public CommandHandler(UniversityContext context)
            {
                _context = context;
            }

            private readonly UniversityContext _context;

            protected override async Task HandleCore(Command message)
            {
                var instructorInDb = await _context
                    .Instructors
                    .FirstOrDefaultAsync(i => i.Id == message.Id);

                _context.Instructors.Remove(instructorInDb);
            }
        }
    }
}
