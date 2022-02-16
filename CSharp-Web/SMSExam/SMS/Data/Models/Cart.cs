namespace SMS.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class Cart
    {
        public Cart()
        {
            this.Products = new HashSet<Product>();
        }

        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public User User { get; init; }

        public ICollection<Product> Products { get; init; }
    }
}
