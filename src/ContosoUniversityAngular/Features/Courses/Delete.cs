namespace ContosoUniversityAngular.Features.Courses
{
    using Data;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class Delete
    {
        public class Command : IAsyncRequest
        {
            public int Id { get; set; }
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
                var course = await _context
                    .Courses
                    .FirstOrDefaultAsync(c => c.Id == message.Id);

                if (course != null)
                    _context.Courses.Remove(course);
            }
        }
    }
}

