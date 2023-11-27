using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Lab_4.Models
{
    public class User : IdentityUser
    {
        [Required]
        [Range(1910, 2050)]
        public int Year { get; set; }
    }
}
