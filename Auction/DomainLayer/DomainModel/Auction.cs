// <copyright file="Auction.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace Auction.DomainLayer.DomainModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Currency enum.
    /// </summary>
    public enum Currency
    {
        /// <summary>
        /// The euro.
        /// </summary>
        Euro,

        /// <summary>
        /// The dolar.
        /// </summary>
        Dolar,

        /// <summary>
        /// The ron.
        /// </summary>
        Ron,
    }

    /// <summary>Class used to define an auction.</summary>
    public class Auction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Auction"/> class.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="product">The product.</param>
        /// <param name="currency">The currency.</param>
        public Auction(DateTime startTime, DateTime endTime, Product product, Currency currency)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.IsFinished = false;

            this.Product = product;
            this.Currency = currency;
        }

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [Key]
        public int Id { get; set; }

        /// <summary>Gets or sets the offerer.</summary>
        /// <value>The offerer.</value>
        [Required(ErrorMessage = "The offerer cannot be null")]
        public User Offerer { get; set; }

        /// <summary>Gets or sets the product.</summary>
        /// <value>The product.</value>
        [Required(ErrorMessage = "The product cannot be null")]
        public Product Product { get; set; }

        /// <summary>
        /// Gets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        [Required]
        [Range(0, 2)]
        public Currency Currency { get; }

        /// <summary>Gets the start time.</summary>
        /// <value>The start time.</value>
        [Required(ErrorMessage = "Start date cannot be null")]
        public DateTime StartTime { get; }

        /// <summary>Gets the end time.</summary>
        /// <value>The end time.</value>
        [Required(ErrorMessage = "End date cannot be null")]
        public DateTime EndTime { get; }

        /// <summary>
        /// Gets or sets the current price.
        /// </summary>
        /// <value>
        /// The current price.
        /// </value>
        [Required]
        public decimal CurrentPrice
        {
            get
            {
                return this.CurrentPrice;
            }

            set
            {
                if (value < 3 * this.CurrentPrice)
                {
                    this.CurrentPrice = value;
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether this instance is finished.</summary>
        /// <value>
        ///   <c>true</c> if this instance is finished; otherwise, <c>false</c>.</value>
        public bool IsFinished { get; set; }
    }
}
