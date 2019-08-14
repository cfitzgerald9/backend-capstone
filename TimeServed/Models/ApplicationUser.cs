using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TimeServed.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
   
        public ApplicationUser()
        {

        }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string UserRole { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        [Display(Name = "Employee ID")]
        public int? EmployeeId { get; set; }
        [Display(Name = "Attorney ID")]
        public int? AttorneyId { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

    }
}
