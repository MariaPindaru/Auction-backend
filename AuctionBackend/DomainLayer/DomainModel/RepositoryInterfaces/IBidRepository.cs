// <copyright file="IBidRepository.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DataLayer.DataAccessLayer.Interfaces
{
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// Interface used for defining the functionalities of a repository for the entity  Bid.
    /// </summary>
    /// <seealso cref="AuctionBackend.DataLayer.DataAccessLayer.Interfaces.IRepository&lt;AuctionBackend.DomainLayer.DomainModel.Bid&gt;" />
    public interface IBidRepository : IRepository<Bid>
    {
    }
}
