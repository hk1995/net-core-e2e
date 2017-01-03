using System;
using System.Threading.Tasks;
using LMS.Data;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LMS.Web.Infrastructure.Filters
{
    public class DbContextTransactionFilter : IAsyncActionFilter
    {
        private readonly LmsContext _dbContext;

        public DbContextTransactionFilter(LmsContext dbContext)
        {
            _dbContext = dbContext;
        }

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
