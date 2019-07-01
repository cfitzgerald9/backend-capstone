using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeServed.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Location")]
        public int LocationId { get; set; }

        public Location location { get; set; }

        public ApplicationUser applicationUser { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
