using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqKit;
using LMS.Core.Domain;

namespace LMS.Core.Extensions
{
    /// <summary>
    /// Extension class for extending Linq queries
    /// </summary>
    public static class QueryExtensions
    {
        #region Generic T

        /// <summary>
        /// Expands IQueryable T to include First entity that is NOT marked as Deleted
        /// </summary>
        /// <typeparam name="T">Entity Type of base EntityModel</typeparam>
        /// <param name="entity">IQueryable T</param>
        /// <returns>FirstOrDefault of type T</returns>
        public static T FirstActive<T>(this IQueryable<T> entity) where T : Entity
        {
            return entity.FirstOrDefault(x => !x.IsDeleted);
        }

        /// <summary>
        /// Expands IQueryable T to include First entity that is NOT marked as Deleted plus addtional query predicate
        /// </summary>
        /// <typeparam name="T">Entity Type of base EntityModel</typeparam>
        /// <param name="entity">IQueryable T</param>
        /// <param name="predicate">Additional query expression</param>
        /// <returns>FirstOrDefault of type T</returns>
        public static T FirstActive<T>(this IQueryable<T> entity, Expression<Func<T, bool>> predicate)
            where T : Entity
        {
            return entity.Where(x => !x.IsDeleted).FirstOrDefault(predicate);
        }

        /// <summary>
        /// Expands IQueryable T to include all entity items that are NOT marked as Deleted plus addtional query predicate
        /// </summary>
        /// <typeparam name="T">Entity Type of base EntityModel</typeparam>
        /// <param name="entity">IQueryable T</param>
        /// <param name="predicate">Additional query expression</param>
        /// <returns>IQueryable of type T</returns>
        public static IQueryable<T> WhereActive<T>(this IQueryable<T> entity, Expression<Func<T, bool>> predicate)
            where T : Entity
        {
            return entity.Where(x => !x.IsDeleted).Where(predicate);
        }

        /// <summary>
        /// Expands IQueryable T to include all entity items that are NOT marked as Deleted
        /// </summary>
        /// <typeparam name="T">Entity Type of base EntityModel</typeparam>
        /// <param name="entity">IQueryable T</param>
        /// <returns>IQueryable of type T</returns>
        public static IQueryable<T> AllActive<T>(this IQueryable<T> entity) where T : Entity
        {
            return entity.Where(x => !x.IsDeleted);
        }

        /// <summary>
        /// Expands IQueryable T to include First entity that is NOT marked as Deleted
        /// </summary>
        /// <typeparam name="T">Entity Type of base EntityModel</typeparam>
        /// <param name="entity">IQueryable T</param>
        /// <returns>Async Task T</returns>
        public static Task<T> FirstActiveAsync<T>(this IQueryable<T> entity) where T : Entity
        {
            return entity.FirstOrDefaultAsync(x => !x.IsDeleted);
        }


        /// <summary>
        /// Expands IQueryable T to include First entity that is NOT marked as Deleted plus addtional query predicate
        /// </summary>
        /// <typeparam name="T">Entity Type of base EntityModel</typeparam>
        /// <param name="entity">IQueryable T</param>
        /// <param name="predicate">Additional query expression</param>
        /// <returns>Async Task T</returns>
        public static Task<T> FirstActiveAsync<T>(this IQueryable<T> entity, Expression<Func<T, bool>> predicate)
            where T : Entity
        {
            return entity.Where(x => !x.IsDeleted).FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Does sequence T contain any matching elements NOT marked as deleted plus additional query predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        /// <returns>Async Task T</returns>
        public static Task<bool> AnyActiveAsync<T>(this IQueryable<T> entity, Expression<Func<T, bool>> predicate)
            where T : Entity
        {
            return entity.Where(x => !x.IsDeleted).AnyAsync(predicate);
        }

        #endregion
    }
}
