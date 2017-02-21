namespace ContosoUniversityAngular.Features.Departments
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Index
    {
        public class Query : IAsyncRequest<Response> { }

        public class Response
        {
            public ICollection<Department> Departments { get; set; }

            public class Department
            {
                public int Id { get; set; }
                
                public string Name { get; set; }
                
                public decimal Budget { get; set; }
                
                public DateTime StartDate { get; set; }

                public string AdministratorFullName { get; set; }
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
                var departments = await _context
                                        .Departments
                                        .Include(d => d.Administrator)
                                        .ProjectTo<Response.Department>()
                                        .ToListAsync();

                return new Response()
                {
                    Departments = departments
                };
            }
        }
    }
}
