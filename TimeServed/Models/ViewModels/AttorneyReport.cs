using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeServed.Models.ViewModels
{
    public class AttorneyReport
    {
        public List<Appointment> appointments { get; set; }
        public List<ApplicationUser> attorneys { get; set; }
        public List<Client> clients { get; set; }

        public List<Location> locations { get; set; }

        public List<TimeSpan> hoursWorked { get; set; } = new List<TimeSpan>();


    }
}
