using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parking.Data;
using Parking.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Controllers
{
    [Authorize(Roles = "Staff")]
    public class ParkingEntitiesController : Controller
    {
        private readonly AppDbContext _context;

        public ParkingEntitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ParkingEntities
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Parkings.Include(p => p.User).Include(p => p.Vehicle);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ParkingEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingEntity = await _context.Parkings
                .Include(p => p.User)
                .Include(p => p.Vehicle)
                .FirstOrDefaultAsync(m => m.ParkingId == id);
            if (parkingEntity == null)
            {
                return NotFound();
            }

            return View(parkingEntity);
        }

        // GET: ParkingEntities/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users.Where(u => u.Role == "Apprentice"), "UserId", "Email");
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "Plate");
            ViewData["SpotId"] = new SelectList(_context.ParkingSpots.Where(s => s.IsAvailable), "SpotId", "SpotCode");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId,UserId,SpotId")] ParkingEntity parkingEntity)
        {
            if (ModelState.IsValid)
            {
                parkingEntity.EntryTime = DateTime.Now;
                parkingEntity.Status = "Inside";

                // Marcar el lugar como ocupado
                var spot = await _context.ParkingSpots.FindAsync(parkingEntity.SpotId);
                if (spot != null) spot.IsAvailable = false;

                _context.Add(parkingEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parkingEntity);
        }


        // GET: ParkingEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingEntity = await _context.Parkings.FindAsync(id);
            if (parkingEntity == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", parkingEntity.UserId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "Plate", parkingEntity.VehicleId);
            return View(parkingEntity);
        }

        // POST: ParkingEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParkingId,VehicleId,UserId,EntryTime,ExitTime,Status,TotalHours,PaidAmount")] ParkingEntity parkingEntity)
        {
            if (id != parkingEntity.ParkingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkingEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingEntityExists(parkingEntity.ParkingId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", parkingEntity.UserId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "Plate", parkingEntity.VehicleId);
            return View(parkingEntity);
        }

        // GET: ParkingEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingEntity = await _context.Parkings
                .Include(p => p.User)
                .Include(p => p.Vehicle)
                .FirstOrDefaultAsync(m => m.ParkingId == id);
            if (parkingEntity == null)
            {
                return NotFound();
            }

            return View(parkingEntity);
        }

        // POST: ParkingEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkingEntity = await _context.Parkings.FindAsync(id);
            if (parkingEntity != null)
            {
                _context.Parkings.Remove(parkingEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkingEntityExists(int id)
        {
            return _context.Parkings.Any(e => e.ParkingId == id);
        }
    }
}
