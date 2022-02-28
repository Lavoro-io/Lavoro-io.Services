using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UserService.DAL
{
    [Table("Users")]
    [Index(nameof(Username), nameof(Email))]
    public class UserDAL
    {
        [Key]
        public Guid UserId { get; set; }

        [Required, MaxLength(32)]
        public string Username { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required, MaxLength(32), MinLength(8)]
        public string Password { get; set; }

        public bool Enabled { get; set; }
    }
}

