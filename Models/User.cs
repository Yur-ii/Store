using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Store.Models
{
    public class User : IdentityUser
    {
        [Required]
        public DateTime DateOfBirth { get; set; }
        public byte[] Image { get; set; }
    }
}
