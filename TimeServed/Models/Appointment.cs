using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeServed.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Attorney")]
        public string ApplicationUserId { get; set; }
        [Required]
        [Display(Name = "Client")]
        public int ClientId { get; set; }
        [Required]
        [Display(Name = "Scheduled Date")]
        public DateTime VisitDate { get; set; }
        [Display(Name = "Check-in")]
        public DateTime? CheckIn { get; set; }
        [Display(Name = "Check-out")]
        public DateTime? CheckOut { get; set; }

        public TimeSpan TimeSpent()
        {
            var x = this.CheckOut.Value - this.CheckIn.Value;
            return x;
        }

        [Display(Name = "Client")]
        public Client client { get; set; }
        [Display(Name = "Attorney")]
        public ApplicationUser applicationUser { get; set; }
    }
}
