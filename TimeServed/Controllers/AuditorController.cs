using System;
using System.Collections.Generic;
using System.Data;
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
    public class AuditorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuditorsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        // GET: Appointments
        [Authorize(Roles = "Auditor")]
        public async Task<IActionResult> Index(string nameString, int? searchDate)
        {
            var currentUser = GetCurrentUserAsync();
            var applicationDbContext = await _context.Appointments
                .Include(o => o.client)
                .Include(o => o.client.location)
                .Include(o => o.applicationUser)
                .ToListAsync();
            if (nameString != null)
            {
                applicationDbContext = applicationDbContext.Where(a => a.applicationUser.FirstName.Contains(nameString) || a.applicationUser.LastName.Contains(nameString)).ToList();

            }
            if (searchDate != null)
            {
                applicationDbContext = applicationDbContext.Where(p => p.VisitDate.Month == searchDate || p.VisitDate.Year == searchDate || p.VisitDate.Day == searchDate).ToList();
            }


            return View(applicationDbContext.ToList());
        }

        [Authorize(Roles = "Auditor")]
        public async Task<IActionResult> Hours(AttorneyReport model)
        {
            model.appointments = _context.Appointments.Where(a => a.CheckIn != null && a.CheckOut != null).Include(a => a.applicationUser).ToList();
            model.attorneys = _context.ApplicationUsers.Where(u => u.UserRole == "Attorney").ToList();
            List<string> names = new List<string>();
            TimeSpan emptyTime = TimeSpan.Zero;
            Dictionary<string, List<Appointment>> appointmentList = new Dictionary<string, List<Appointment>>();
            List<Appointment> filteredAppointments = new List<Appointment>();
            foreach (Appointment a in model.appointments)
            {

                emptyTime = a.TimeSpent() + emptyTime;
                filteredAppointments = model.appointments.FindAll(b => b.ApplicationUserId == a.ApplicationUserId);
                if (appointmentList.ContainsKey(a.ApplicationUserId) == false)
                {
                    appointmentList.Add(a.ApplicationUserId, filteredAppointments);

                }
                else
                {
                    appointmentList.TryAdd(a.ApplicationUserId, filteredAppointments);
                }

            }
          
            foreach (ApplicationUser au in model.attorneys)
            {
                string attorneyName = au.FirstName + " " + au.LastName;
                names.Add(attorneyName);
            }
         
            var testHours = emptyTime.TotalHours;
            model.hoursWorked.Add(testHours);

            ViewBag.hours = model.hoursWorked;
            ViewBag.appointments = appointmentList;
            ViewBag.names = names;
            return View(model);
        }


        // GET: Appointments/Details/5
        [Authorize(Roles = "Auditor")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(o => o.client)
                .Include(o => o.applicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Delete/5
        [Authorize(Roles = "Auditor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var appointment = await _context.Appointments
                .Include(a => a.applicationUser)
                .Include(a => a.client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

         
           
            
            else return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Auditor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
