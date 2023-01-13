// <copyright file="BaseRepository.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DataLayer.DataAccessLayer.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;

    /// <summary>
    /// BaseRepository.
    /// </summary>
    /// <typeparam name="T">Entity type.</typeparam>
    /// <seealso cref="AuctionBackend.DataLayer.DAL.Interfaces.IRepository&lt;T&gt;" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>
        /// Collection of entities.
        /// </returns>
        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            using (AppDbContext ctx = new AppDbContext())
            {
                var dbSet = ctx.Set<T>();

                IQueryable<T> query = dbSet;

                foreach (var includeProperty in includeProperties.Split(
                   new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Insert(T entity)
        {
            using (var ctx = new AppDbContext())
            {
                var dbSet = ctx.Set<T>();
                dbSet.Attach(entity);

                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual void Update(T item)
        {
            using (var ctx = new AppDbContext())
            {
                var dbSet = ctx.Set<T>();
                dbSet.Attach(item);
                ctx.Entry(item).State = EntityState.Modified;

                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void Delete(object id)
        {
            this.Delete(this.GetByID(id));
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entityToDelete"> entity to delete. </param>
        public virtual void Delete(T entityToDelete)
        {
            using (var ctx = new AppDbContext())
            {
                var dbSet = ctx.Set<T>();

                if (ctx.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }

                dbSet.Remove(entityToDelete);

                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The object.
        /// </returns>
        public virtual T GetByID(object id)
        {
            using (var ctx = new AppDbContext())
            {
                return ctx.Set<T>().Find(id);
            }
        }
    }
}
