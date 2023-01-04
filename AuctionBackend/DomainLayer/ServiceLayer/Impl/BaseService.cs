// <copyright file="BaseService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Impl
{
    using System.Collections.Generic;
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;

    using FluentValidation;
    using FluentValidation.Results;
    using log4net;

    /// <summary>
    /// BaseService.
    /// </summary>
    /// <typeparam name="T"> Repository type. </typeparam>
    /// <typeparam name="TU"> Validator type. </typeparam>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IService&lt;T&gt;" />
    public abstract class BaseService<T, TU> : IService<T>
        where T : class
        where TU : IRepository<T>
    {
        /// <summary>
        /// The repository.
        /// </summary>
        protected TU repository;

        /// <summary>
        /// The validator.
        /// </summary>
        protected IValidator<T> validator;

        private static readonly ILog Logger = LogManager.GetLogger($"{typeof(T).Name}BaseService");

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{T, TU}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="validator">The validator.</param>
        public BaseService(TU repository, IValidator<T> validator)
        {
            this.repository = repository;
            this.validator = validator;

            Logger.Info("Service created.");
        }

        /// <summary>Inserts the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   The validation result.
        /// </returns>
        public ValidationResult Insert(T entity)
        {
            var result = this.validator.Validate(entity);
            if (result.IsValid)
            {
                this.repository.Insert(entity);
                Logger.Info($"An object of type {typeof(T).Name} has been inserted.");
            }
            else
            {
                IList<ValidationFailure> failures = result.Errors;
                Logger.Error($"The object of type {typeof(T).Name} is not valid. The following errors occurred: {failures}");
            }

            return result;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The validation result.</returns>
        public ValidationResult Update(T entity)
        {
            var result = this.validator.Validate(entity);
            if (result.IsValid)
            {
                this.repository.Update(entity);
            }
            else
            {
                IList<ValidationFailure> failures = result.Errors;
                Logger.Error($"The object of type {typeof(T).Name} is not valid. The following errors occurred: {failures}");
            }

            return result;
        }

        /// <summary>Deletes the specified entity.</summary>
        /// <param name="entity">The entity to be deleted.</param>
        public void Delete(T entity)
        {
            this.repository.Delete(entity);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The object found.</returns>
        public T GetByID(object id)
        {
            return this.repository.GetByID(id);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of objects found.</returns>
        public IEnumerable<T> GetAll()
        {
            return this.repository.Get();
        }
    }
}
