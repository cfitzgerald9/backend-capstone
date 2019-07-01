using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeServed.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using TimeServed.Data;
using Microsoft.EntityFrameworkCore;
using TimeServed.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeServed.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(_userManager.Users);
        }
        // GET: Users/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var applicationUser = await _context.ApplicationUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: Users/Create
        [Authorize]
        public IActionResult Create()
        {

            CreateUserWithRoleViewModel createUserModel = new CreateUserWithRoleViewModel();
            SelectList allRoles = new SelectList(_context.Roles, "Name", "Name");
            // Add a 0 option to the select list
            SelectList roles0 = RoleDropdown(allRoles);

           createUserModel.Roles = roles0;
            return View(createUserModel);


        }


        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserWithRoleViewModel createUser)
        {
            if (ModelState.IsValid)
            {
                var newUser = new ApplicationUser()
                {
                    Email = createUser.applicationUser.Email,
                    UserName = createUser.applicationUser.UserName,
                    FirstName = createUser.applicationUser.FirstName,
                    LastName = createUser.applicationUser.LastName,
                    EmployeeId = createUser.applicationUser.EmployeeId,
                    UserRole = createUser.applicationUser.UserRole,
                    StreetAddress = createUser.applicationUser.StreetAddress
                };
                var user = await _userManager
                       .CreateAsync(newUser, "Password1234!");
                await _userManager.AddToRoleAsync(newUser, createUser.applicationUser.UserRole);
                await _context.SaveChangesAsync();
                return Index();
            }

            // If we got this far, something failed, redisplay form
            return View(createUser);
        }
        public static SelectList RoleDropdown(SelectList selectList)
        {

            SelectListItem firstItem = new SelectListItem()
            {
                Text = "Select a role"
            };
            List<SelectListItem> newList = selectList.ToList();
            newList.Insert(0, firstItem);

            var selectedItem = newList.FirstOrDefault(item => item.Selected);
            var selectedItemValue = String.Empty;
            if (selectedItem != null)
            {
                selectedItemValue = selectedItem.Value;
            }

            return new SelectList(newList, "Value", "Text", selectedItemValue);
        }
    }
}