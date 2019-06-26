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
        public DateTime VisitDate { get; set; }

        public DateTime? CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }

        public Client client { get; set; }
        public ApplicationUser applicationUser { get; set; }
    }
}
