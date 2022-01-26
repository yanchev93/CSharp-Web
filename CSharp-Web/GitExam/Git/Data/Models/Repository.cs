namespace Git.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Repository
    {
        public Repository()
        {
            this.Commits = new HashSet<Commit>();
        }

        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Name { get; set; }

        public DateTime CreatedOn { get; init; }

        public bool IsPublic { get; set; }

        [Required]
        public string OwnerId { get; init; }

        public User Owner { get; init; }
        
        public ICollection<Commit> Commits { get; init; }

    }
}


//•	Has an Id – a string, Primary Key
//•	Has a Name – a string with min length 3 and max length 10 (required)
//•	Has a CreatedOn – a datetime (required)
//•	Has a IsPublic – bool (required)
//•	Has a OwnerId – a string
//•	Has a Owner – a User object
//•	Has Commits collection – a Commit type
