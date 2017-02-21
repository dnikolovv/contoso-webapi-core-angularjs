namespace ContosoUniversityAngular.Features.Departments
{
    using AutoMapper;
    using Data;
    using FluentValidation;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
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

            public string Name { get; set; }

            public decimal Budget { get; set; }
            
            public DateTime StartDate { get; set; }
            
            public Instructor Administrator { get; set; }

            public class Instructor
            {
                public int Id { get; set; }

                public string FirstName { get; set; }

                public string LastName { get; set; }

                public string FullName { get; set; }
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
                var depInDb = await _context
                    .Departments
                    .Include(d => d.Administrator)
                    .FirstOrDefaultAsync(d => d.Id == message.Id);

                return Mapper.Map<Response>(depInDb);
            }
        }
    }
}
