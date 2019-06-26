using TimeServed.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeServed.Models.ViewModels
{
    public class RegisterUserWithTypeViewModel
    {
        public RegisterUserWithTypeViewModel() {
    }
        public ApplicationUser ApplicationUser { get; set; }

        public SelectList userTypes { get; set; }
    }
}
