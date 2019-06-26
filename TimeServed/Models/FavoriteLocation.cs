using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeServed.Models
{
    public class FavoriteLocation
    {
        public int Id { get; set; }
        [Required]
        public int ApplicationUserId { get; set; }
        [Required]
        public int LocationId { get; set; }
    }
}
