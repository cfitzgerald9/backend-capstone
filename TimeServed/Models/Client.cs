﻿using System;
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
        [StringLength(20)]
        [Display(Name = "First name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Location")]
        public int LocationId { get; set; }
       
        public Location location { get; set; }

        [Display(Name = "Attorney")]
        public ApplicationUser applicationUser { get; set; }
        [Display(Name = "Attorney ID")]
        public string ApplicationUserId { get; set; }
    }
}
