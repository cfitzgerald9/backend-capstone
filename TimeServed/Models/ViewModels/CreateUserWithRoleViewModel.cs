using TimeServed.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeServed.Models.ViewModels
{
    public class CreateUserWithRoleViewModel
    {
        public CreateUserWithRoleViewModel() {
    }
        public ApplicationUser applicationUser { get; set; }

        public SelectList Roles { get; set; }
    }
}
