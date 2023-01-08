// <copyright file="UserScore.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// UserScore.
    /// </summary>
    public class UserScore
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the scored user.
        /// </summary>
        /// <value>
        /// The scored user.
        /// </value>
        [Required]
        public User ScoredUser { get; set; }

        /// <summary>
        /// Gets or sets the scoring user.
        /// </summary>
        /// <value>
        /// The scoring user.
        /// </value>
        [Required]
        public User ScoringUser { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        [Required]
        public int Score { get; set; }
    }
}
