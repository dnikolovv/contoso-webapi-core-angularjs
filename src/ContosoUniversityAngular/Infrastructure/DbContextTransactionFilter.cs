namespace ContosoUniversityAngular.Infrastructure
{
    using Data;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Threading.Tasks;

    public class DbContextTransactionFilter : IAsyncActionFilter
    {
        public DbContextTransactionFilter(UniversityContext context)
        {
            _dbContext = context;
        }

        private readonly UniversityContext _dbContext;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                _dbContext.BeginTransaction();

                await next();

                await _dbContext.CommitTransactionAsync();
            }
            catch (Exception)
            {
                _dbContext.RollbackTransaction();
                throw;
            }
        }
    }
}
