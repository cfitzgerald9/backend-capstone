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
        public string ApplicationUserId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        [Display(Name = "Scheduled date")]
        public DateTime VisitDate { get; set; }
        [Display(Name = "Check-in")]
        public DateTime? CheckIn { get; set; }
        [Display(Name = "Check-out")]
        public DateTime? CheckOut { get; set; }

        public Client client { get; set; }
        public ApplicationUser applicationUser { get; set; }
    }
}
