// <copyright file="BaseService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Impl
{
    using System.Collections.Generic;
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.Config;
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
        /// The logger.
        /// </summary>
        protected static readonly ILog Logger = LogManager.GetLogger($"{typeof(T).Name}BaseService");

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{T, TU}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="validator">The validator.</param>
        public BaseService(TU repository, IValidator<T> validator)
        {
            this.Repository = repository;
            this.Validator = validator;

            Logger.Info("Service created.");
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        protected TU Repository { get; }

        /// <summary>
        /// Gets the validator.
        /// </summary>
        /// <value>
        /// The validator.
        /// </value>
        protected IValidator<T> Validator { get; }

        /// <summary>Inserts the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   The validation result.
        /// </returns>
        public virtual ValidationResult Insert(T entity)
        {
            Logger.Info($"Inserting object of type {typeof(T).Name}.");

            var result = this.Validator.Validate(entity);
            if (result.IsValid)
            {
                this.Repository.Insert(entity);
                Logger.Info($"The object has been inserted.");
            }
            else
            {
                IList<ValidationFailure> failures = result.Errors;
                Logger.Error($"The object is not valid. The following errors occurred: {failures}");
            }

            return result;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The validation result.</returns>
        public virtual ValidationResult Update(T entity)
        {
            Logger.Info($"Updating object of type {typeof(T).Name}.");

            var result = this.Validator.Validate(entity);
            if (result.IsValid)
            {
                this.Repository.Update(entity);
                Logger.Error($"The object has been updated.");
            }
            else
            {
                IList<ValidationFailure> failures = result.Errors;
                Logger.Error($"The object is not valid. The following errors occurred: {failures}");
            }

            return result;
        }

        /// <summary>Deletes the specified entity.</summary>
        /// <param name="entity">The entity to be deleted.</param>
        public void Delete(T entity)
        {
            Logger.Info($"Deleting object of type {typeof(T).Name}.");
            this.Repository.Delete(entity);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The object found.</returns>
        public T GetByID(object id)
        {
            Logger.Info($"Geting by id object of type {typeof(T).Name}.");
            return this.Repository.GetByID(id);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of objects found.</returns>
        public IEnumerable<T> GetAll()
        {
            Logger.Info($"Getting all objects of type {typeof(T).Name}.");
            return this.Repository.Get();
        }
    }
}
