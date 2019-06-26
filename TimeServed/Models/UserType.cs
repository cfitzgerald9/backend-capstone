using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeServed.Models
{
    public class UserType
    {
        public UserType()
        {

        }
        public int Id {get; set;}
        [Required]
        public string Role { get; set; }

    }
}
