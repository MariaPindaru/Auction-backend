// <copyright file="CategoryRepository.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DataLayer.DataAccessLayer.Impl
{
    using System.Collections.Generic;
    using System.Linq;
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// CategoryRepository.
    /// </summary>
    /// <seealso cref="AuctionBackend.DataLayer.DataAccessLayer.Impl.BaseRepository&lt;AuctionBackend.DomainLayer.DomainModel.Category&gt;" />
    /// <seealso cref="AuctionBackend.DataLayer.DAL.Interfaces.ICategoryRepository" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Insert(Category entity)
        {
            using (var ctx = new AppDbContext())
            {
                Category aux;
                var parentsList = new HashSet<Category>();
                foreach (var parent in entity.Parents)
                {
                    aux = ctx.Set<Category>().Find(parent.Id);
                    if (aux != null)
                    {
                        parentsList.Add(aux);
                    }
                    else
                    {
                        parentsList.Add(parent);
                    }
                }

                var childrenList = new HashSet<Category>();
                foreach (var child in entity.Children)
                {
                    aux = ctx.Set<Category>().Find(child.Id);
                    if (aux != null)
                    {
                        childrenList.Add(aux);
                    }
                    else
                    {
                        childrenList.Add(child);
                    }
                }

                entity.Parents = parentsList;
                entity.Children = childrenList;

                ctx.Set<Category>().Add(entity);

                ctx.SaveChanges();
            }
        }
    }
}
