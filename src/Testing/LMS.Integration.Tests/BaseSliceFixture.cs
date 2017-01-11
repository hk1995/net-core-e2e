using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LMS.Core.Domain;
using LMS.Data;
using LMS.Web.Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace LMS.Integration.Tests
{
    public abstract class BaseSliceFixture
    {
        protected static Checkpoint _checkpoint;
        protected static IConfigurationRoot _configuration;
        protected static  IServiceScopeFactory _scopeFactory;

        public static Checkpoint ConfigureCheckpoint()
        {
            return new Checkpoint
            {
                TablesToIgnore = new[] { "__MigrationHistory" }
            };
        }

        public static void ResetCheckpoint()
        {
            _checkpoint.Reset(_configuration.GetConnectionString(AppSettings.ConnectionStringName));
        }

        public async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContent = scope.ServiceProvider.GetService<LmsContext>();
                try
                {
                    dbContent.BeginTransaction();
                    await action(scope.ServiceProvider);
                    await dbContent.CommitTransactionAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    dbContent.RollbackTransaction();
                    throw;
                }
            }
        }

        public async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<LmsContext>();
                try
                {
                    dbContext.BeginTransaction();

                    var result = await action(scope.ServiceProvider);
                    await dbContext.CommitTransactionAsync();

                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    dbContext.RollbackTransaction();
                    throw;
                }
            }
        }

        public Task ExecuteDbContextAsync(Func<LmsContext, Task> action)
        {
            return ExecuteScopeAsync(sp => action(sp.GetService<LmsContext>()));
        }

        public Task<T> ExecuteDbContextAsync<T>(Func<LmsContext, Task<T>> action)
        {
            return ExecuteScopeAsync(sp => action(sp.GetService<LmsContext>()));
        }

        public Task InsertAsync<T>(params T[] entities) where T : class
        {
            return ExecuteDbContextAsync(async ctx =>
            {
                ctx.Set<T>().AddRange(entities);
                await ctx.SaveChangesAsync();
            });
        }

        public Task UpdateAsync<T>(params T[] entities) where T : class
        {
            return ExecuteDbContextAsync(async ctx =>
            {
                foreach (var updated in entities)
                {
                    ctx.Set<T>().Attach(updated);
                    ctx.Entry(updated).State = EntityState.Modified;
                }
                await ctx.SaveChangesAsync();
            });
        }

        public async Task<T> FindAsync<T>(object id) where T : class
        {
            T entity = null;
            await ExecuteDbContextAsync(async ctx =>
            {
                entity = await ctx.Set<T>().FindAsync(id);
            });

            return entity;
        }

        public async Task<T> GetAsync<T>(long id, params Expression<Func<T, object>>[] includeExpression) where T : Entity
        {
            T entity = null;
            await ExecuteDbContextAsync(async ctx =>
            {
                var set = ctx.Set<T>();
                entity = await IncludeAll(set, includeExpression).FirstOrDefaultAsync(x => x.Id == id);
            });

            return entity;
        }

        public async Task<TResponse> SendAsync<TResponse>(IAsyncRequest<TResponse> request)
        {
            var response = default(TResponse);
            await ExecuteScopeAsync(async sp =>
            {
                var mediator = sp.GetService<IMediator>();
                response = await mediator.SendAsync(request);
            });
            return response;
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            var response = default(TResponse);
            await ExecuteScopeAsync(sp =>
            {
                var mediator = sp.GetService<IMediator>();
                response = mediator.Send(request);

                return Task.FromResult(0);
            });
            return response;
        }

        private static IQueryable<T> IncludeAll<T>(IQueryable<T> dbSet, params Expression<Func<T, object>>[] includeExpressions)
            where T : Entity
        {
            foreach (var includeExpr in includeExpressions)
            {
                dbSet = dbSet.Include(includeExpr);
            }

            return dbSet;
        }
    }
}
