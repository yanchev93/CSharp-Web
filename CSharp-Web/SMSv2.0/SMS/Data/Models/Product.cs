namespace SMS.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class Product
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(DefaultMaxLength, MinimumLength = ProductMinName)]
        public string Name { get; set; }

        [Required]
        [Range(0.05, 1000)]
        public decimal Price { get; set; }

        [Required]
        public string CartId { get; set; }

        [Required]
        public Cart Cart { get; set; }
    }
}
