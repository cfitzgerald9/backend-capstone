using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeServed.Data;
using TimeServed.Models;
using TimeServed.Models.ViewModels;

namespace TimeServed.Controllers
{
    public class GuardsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GuardsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Guards
        [Authorize(Roles = "Guard")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Clients.Include(c => c.applicationUser).Include(c => c.location).Where(c=> c.isActive == true);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "Guard")]
        public async Task<IActionResult> Appointments()
        {
            var applicationDbContext = _context.Appointments
                .Include(o => o.client)
                .Include(a => a.applicationUser)
                .Include(a => a.client.location)
                .OrderBy(a => a.VisitDateStart);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: Guards/Details/5
        [Authorize(Roles ="Guard")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.applicationUser)
                .Include(c => c.location)
                .Where(c => c.isActive == true)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Guards/Create
        [Authorize(Roles = "Guard")]
        public IActionResult Create()
        {
            
            CreateClientWithLocationAndAttorneyViewModel vm = new CreateClientWithLocationAndAttorneyViewModel();
            SelectList attorneys = new SelectList(_context.ApplicationUsers.Where(c => c.UserRole == "Attorney"), "Id", "FullName");
            // Add a 0 option to the select list
            SelectList attorneys0 = AttorneyDropdown(attorneys);
            vm.applicationUsers = attorneys0;
            SelectList locations = new SelectList(_context.Locations, "Id", "LocationName");
            // Add a 0 option to the select list
            SelectList locations0 = AttorneyDropdown(locations);
            vm.locations = locations0;
            return View(vm);
        }

        // POST: Guards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Guard")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,LocationId,ApplicationUserId")] Client client)
        {
            if (ModelState.IsValid)
            {
                client.isActive = true;
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers.Where(c => c.UserRole == "Attorney"), "Id", "LastName", client.ApplicationUserId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "LocationName", client.LocationId);
            return View(client);
        }


        // GET: Guards/Delete/5
        [Authorize(Roles = "Guard")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.applicationUser)
                .Include(c => c.location)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Guards/Delete/5
        [Authorize(Roles = "Guard")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            client.isActive = false;
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public static SelectList AttorneyDropdown(SelectList selectList)
        {

            SelectListItem firstItem = new SelectListItem()
            {
                Text = "N/a"
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
