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
            this.Products = new List<Product>();
        }

        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string UserId { get; set; }

        [Required]
        public User User { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
