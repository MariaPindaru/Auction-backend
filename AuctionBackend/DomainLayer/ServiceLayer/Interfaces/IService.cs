namespace AuctionBackend.DomainLayer.ServiceLayer.Interfaces
{
    using System.Collections.Generic;
    using FluentValidation.Results;

    /// <summary>
    /// IService.
    /// </summary>
    /// <typeparam name="T"> Entity type. </typeparam>
    public interface IService<T>
        where T : class
    {
        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns> ValidationResult. </returns>
        ValidationResult Insert(T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns> ValidationResult. </returns>
        ValidationResult Update(T entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns> The found object or null otherwise. </returns>
        T GetByID(object id);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns> IEnumerable of all objects. </returns>
        IEnumerable<T> GetAll();
    }
}
