using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GlobalService.DAL
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

        [Required, MinLength(8)]
        public string Password { get; set; }

        public Guid RoleId { get; set; }
        public RoleDAL Role { get; set; }

        public bool Enabled { get; set; }
    }
}

