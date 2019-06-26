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
        public int LocationName { get; set; }
        [Required]
        public string StreetAddress { get; set; }
    }
}
