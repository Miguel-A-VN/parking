using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parking.Data;
using Parking.Models.Entities;

namespace Parking.Controllers
{
    public class ReservationEntitiesController : Controller
    {
        private readonly AppDbContext _context;

        public ReservationEntitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReservationEntities
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Reservations.Include(r => r.Vehicle);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ReservationEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationEntity = await _context.Reservations
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservationEntity == null)
            {
                return NotFound();
            }

            return View(reservationEntity);
        }

        // GET: ReservationEntities/Create
        public IActionResult Create()
        {
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "Plate");
            return View();
        }

        // POST: ReservationEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationId,VehicleId,ReservedAt,ExpiresAt,Status")] ReservationEntity reservationEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservationEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "Plate", reservationEntity.VehicleId);
            return View(reservationEntity);
        }

        // GET: ReservationEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationEntity = await _context.Reservations.FindAsync(id);
            if (reservationEntity == null)
            {
                return NotFound();
            }
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "Plate", reservationEntity.VehicleId);
            return View(reservationEntity);
        }

        // POST: ReservationEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationId,VehicleId,ReservedAt,ExpiresAt,Status")] ReservationEntity reservationEntity)
        {
            if (id != reservationEntity.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservationEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationEntityExists(reservationEntity.ReservationId))
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
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "Plate", reservationEntity.VehicleId);
            return View(reservationEntity);
        }

        // GET: ReservationEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationEntity = await _context.Reservations
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservationEntity == null)
            {
                return NotFound();
            }

            return View(reservationEntity);
        }

        // POST: ReservationEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservationEntity = await _context.Reservations.FindAsync(id);
            if (reservationEntity != null)
            {
                _context.Reservations.Remove(reservationEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationEntityExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationId == id);
        }
    }
}
