using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeServed.Models
{
    public class Appointment : IValidatableObject
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
        public DateTime VisitDateStart { get; set; }
        [Required]
        [Display(Name = "Scheduled Ending")]
        public DateTime VisitDateEnd { get; set; }
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

        public string Notes { get; set; }

        public string CaseNumber { get; set; }

        public string Purpose { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (VisitDateStart.Date < DateTime.Now.Date)
            {
                yield return new ValidationResult(
                    $"Appointments must be booked at least a day ahead of time.",
                    new[] { "VisitDateStart" });
            }
            if(VisitDateEnd < VisitDateStart)
            {
                yield return new ValidationResult(
                    $"End date must be later than start date.",
                    new[] { "VisitDateEnd" });
            }
        }

    }
}
