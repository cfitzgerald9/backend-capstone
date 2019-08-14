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
    public class VisitorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public VisitorController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        // GET: Appointments
        [Authorize(Roles = "Visitor")]
        public async Task<IActionResult> Index()
        {
            var currentUser = await GetCurrentUserAsync();
            var applicationDbContext = _context.Appointments
                .Include(o => o.client)
                .Where(a => a.ApplicationUserId == currentUser.Id)
                .OrderBy(a => a.VisitDate);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Previous()
        {
            var currentUser = await GetCurrentUserAsync();
            var applicationDbContext = _context.Appointments
                .Include(o => o.client)
                .Where(a => a.ApplicationUserId == currentUser.Id)
                .Where(a => a.VisitDate < DateTime.Now)
                .OrderBy(a => a.VisitDate);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        [Authorize(Roles = "Visitor")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(o => o.client)
                .Include(o => o.client.location)
                .Include(o => o.applicationUser)
                .Where(o => o.client.isActive == true)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        [Authorize(Roles = "Visitor")]
        public async Task<IActionResult> Create()
        {
            var currentUser = await GetCurrentUserAsync();
            AppointmentClientViewModel vm = new AppointmentClientViewModel();
            SelectList clients = new SelectList(_context.Clients.Where(c => c.ApplicationUserId == currentUser.Id && c.isActive == true || c.ApplicationUserId == null && c.isActive == true), "Id", "FullName");
            // Add a 0 option to the select list
            SelectList clients0 = ClientDropdown(clients);
            vm.Clients = clients0;
            return View(vm);
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Visitor")]
        public async Task<IActionResult> Create(AppointmentClientViewModel vm)
        {
            var currentUser = await GetCurrentUserAsync();
            SelectList clients = new SelectList(_context.Clients.Where(c => c.isActive == true), "Id", "FullName");
            var appointments = _context.Appointments.ToList();
            List<DateTime> sameTimes = new List<DateTime>();
            foreach(Appointment a in appointments)
            {
                
            };
            // Add a '0' option to the select list
            SelectList clients0 = ClientDropdown(clients);

            ModelState.Remove("appointment.ApplicatationUser");
            ModelState.Remove("appointment.ApplicationUserId");

            if (ModelState.IsValid)
            {
                vm.appointment.ApplicationUserId = currentUser.Id;
                _context.Add(vm.appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                vm.Clients = clients0;
                return View(vm);
            }
        }
        // GET: Appointments/Edit/5
        [Authorize(Roles = "Visitor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            appointment.CheckIn = DateTime.Now;
            _context.Update(appointment);
            await _context.SaveChangesAsync();
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Visitor")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicationUserId,ClientId,VisitDate,CheckIn,CheckOut")] Appointment appointment)
        {
            var currentUser = await GetCurrentUserAsync();

            if (id != appointment.Id)
            {
                return NotFound();
            }
            ModelState.Remove("ApplicationUserId");
            if (ModelState.IsValid)
            {
                try
                {
                    appointment.ApplicationUserId = currentUser.Id;
                    appointment.CheckOut = DateTime.Now;
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }
        public static SelectList ClientDropdown(SelectList selectList)
        {

            SelectListItem firstItem = new SelectListItem()
            {
                Text = "Select a Client"
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

        // GET: Appointments/Delete/5
        [Authorize(Roles = "Visitor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var appointment = await _context.Appointments
                .Include(o => o.client)
                .Include(o => o.client.location)
                .Include(o => o.applicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            if (appointment.VisitDate > DateTime.Now)
            {
                return View(appointment);
            }
            else return View();
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Visitor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }
    }
}
