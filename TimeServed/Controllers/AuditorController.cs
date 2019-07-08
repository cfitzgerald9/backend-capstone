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
        public async Task<IActionResult> Index()
        {
            var currentUser = await GetCurrentUserAsync();
            var applicationDbContext = _context.Appointments
                .Include(o => o.client)
                .Include(o => o.client.location)
                .Include(o => o.applicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "Auditor")]
        public async Task<IActionResult> Hours(AttorneyReport model)
        {
            model.appointments = _context.Appointments.Where(a => a.CheckIn != null && a.CheckOut != null).ToList();
            model.attorneys = _context.ApplicationUsers.Where(u => u.UserRole == "Attorney").ToList();

            ViewBag.apps = model.appointments;
            ViewBag.atts = model.attorneys;
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
