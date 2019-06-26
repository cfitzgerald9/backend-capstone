using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeServed.Models.ViewModels
{
    public class AttorneyReport
    {
        public List<Appointment> appointments { get; set; }
        public List<ApplicationUser> currentUser { get; set; }
        public List<Client> clients { get; set; }

        public List<Location> locations { get; set; }


    }
}
