using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Incorrect email")]
        [Remote(action: "CheckEmail", controller: "Account", ErrorMessage = "Email already used")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
        public int YearOfBirth { get; set; }
        public MonthOfYear MonthOfBirth { get; set; }
        public int DayOfBirth { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]

        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string PasswordConfirm { get; set; }
    }
    public enum MonthOfYear{
        January = 1,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }
}
