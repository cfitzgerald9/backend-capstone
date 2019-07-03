using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeServed.Models
{
    public class Location
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Facility Name")]
        public string LocationName { get; set; }
        [Required]
        [Display(Name = "Facility Name")]
        public string StreetAddress { get; set; }
    }
}
