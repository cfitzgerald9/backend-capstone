using TimeServed.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeServed.Models.ViewModels
{
    public class CreateClientWithLocationAndAttorneyViewModel
    {
        public CreateClientWithLocationAndAttorneyViewModel()
        {
        }
        public Client client { get; set; }
        public SelectList applicationUsers { get; set; }

        public SelectList locations { get; set; }
    }
}
