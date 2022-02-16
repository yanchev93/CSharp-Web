namespace SMS.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class User
    {

        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(DefaultMaxLength, MinimumLength = UserMinUsername)]
        public string Username { get; init; }

        [Required]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }

        [Required]
        public string CartId { get; init; }

        [Required]
        public Cart Cart { get; init; }
    }
}
