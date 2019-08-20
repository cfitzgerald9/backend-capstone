using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeServed.Models.ViewModels
{
    public class AppointmentClientViewModel
    {
        public Appointment appointment { get; set; }

        public SelectList Clients { get; set; }

        public List<SelectListItem> Times { get; set; }

        public string selected { get; set; }


    }
}
