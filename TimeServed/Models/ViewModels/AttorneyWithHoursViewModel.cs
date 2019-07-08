using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeServed.Models.ViewModels
{
    public class AttorneyWithHoursViewModel
    {
        public List<ApplicationUser> attorneys { get; set; }

        public List<TimeSpan> hoursWorked { get; set; }
    }
}
