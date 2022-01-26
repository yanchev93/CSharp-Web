namespace Git.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Commit
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(1000, MinimumLength = 5)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; init; } = DateTime.UtcNow;

        [Required]
        public string CreatorId { get; init; }

        [Required]
        public User Creator { get; init; }

        [Required]
        public string RepositoryId { get; init; }

        [Required]
        public Repository Repository { get; init; }

    }
}

//•	Has an Id – a string, Primary Key
//•	Has a Description – a string with min length 5 (required)
//•	Has a CreatedOn – a datetime (required)
//•	Has a CreatorId – a string
//•	Has Creator – a User object
//•	Has RepositoryId – a string
//•	Has Repository– a Repository object

