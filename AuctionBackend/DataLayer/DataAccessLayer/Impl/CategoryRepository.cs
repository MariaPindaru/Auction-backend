// <copyright file="CategoryRepository.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DataLayer.DAL
{
    using AuctionBackend.DataLayer.DAL.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;
    using System.Data.Entity;

    /// <summary>
    /// CategoryRepository.
    /// </summary>
    /// <seealso cref="AuctionBackend.DataLayer.DAL.BaseRepository&lt;AuctionBackend.DomainLayer.DomainModel.Category&gt;" />
    /// <seealso cref="AuctionBackend.DataLayer.DAL.Interfaces.ICategoryRepository" />
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        ///// <summary>
        ///// Adds the category child.
        ///// </summary>
        ///// <param name="item">The item.</param>
        //public override void Update(Category item)
        //{
        //    using (var ctx = new AppDbContext())
        //    {
        //        var dbSet = ctx.Set<Category>();
        //        dbSet.Attach(item);
        //        ctx.Entry(item).State = EntityState.Modified;

        //        foreach (var child in item.Children)
        //        {
        //            ctx.Entry(child).State = child.Id == 0 ? EntityState.Added : EntityState.Modified;
        //        }

        //        ctx.SaveChanges();
        //    }
        //}
    }
}
