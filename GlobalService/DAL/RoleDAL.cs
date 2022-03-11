using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using GlobalService.Utilities;

namespace GlobalService.DAL
{
    [Table("Roles")]
    [Index(nameof(Name), nameof(RoleEnum))]
    public class RoleDAL : BaseDAL
    {
        [Key]
        public Guid RoleId { get; set; }

        public string Name { get; set; }

        public Roles RoleEnum { get; set; }
    }
}

