namespace SMS.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class Product
    {

        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(DefaultMaxLength, MinimumLength = ProductMinName)]
        public string Name { get; init; }

        [Range(ProductMinPrice, ProductMaxPrice)]
        public decimal Price { get; init; }

        [Required]
        public string CartId { get; init; }

        [Required]
        public Cart Cart { get; init; }
    }
}
