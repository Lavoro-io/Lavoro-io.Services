using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using UserService.Utilities;

namespace UserService.DAL
{
    [Table("Roles")]
    [Index(nameof(Name), nameof(RoleEnum))]
    public class RoleDAL
    {
        [Key]
        public Guid RoleId { get; set; }

        public string Name { get; set; }

        public Roles RoleEnum { get; set; }
    }
}

