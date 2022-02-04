namespace CarShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class Car
    {
        public Car()
        {
            this.Issues = new HashSet<Issue>();
        }

        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(DefaultMaxLength, MinimumLength = CarMinModel)]
        public string Model { get; init; }

        [Required]
        public int Year { get; init; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public string PlateNumber { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<Issue> Issues { get; set; }
    }
}

//•	Has a PlateNumber – a string – Must be a valid Plate number (2 Capital English letters, followed by 4 digits, followed by 2 Capital English letters (required)
//•	Has a OwnerId – a string (required)
//•	Has a Owner – a User object
//•	Has Issues collection – an Issue type

