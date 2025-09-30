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
    public class TariffEntitiesController : Controller
    {
        private readonly AppDbContext _context;

        public TariffEntitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TariffEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tariffs.ToListAsync());
        }

        // GET: TariffEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tariffEntity = await _context.Tariffs
                .FirstOrDefaultAsync(m => m.TariffId == id);
            if (tariffEntity == null)
            {
                return NotFound();
            }

            return View(tariffEntity);
        }

        // GET: TariffEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TariffEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TariffId,VehicleType,Status,HourlyRate")] TariffEntity tariffEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tariffEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tariffEntity);
        }

        // GET: TariffEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tariffEntity = await _context.Tariffs.FindAsync(id);
            if (tariffEntity == null)
            {
                return NotFound();
            }
            return View(tariffEntity);
        }

        // POST: TariffEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TariffId,VehicleType,Status,HourlyRate")] TariffEntity tariffEntity)
        {
            if (id != tariffEntity.TariffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tariffEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TariffEntityExists(tariffEntity.TariffId))
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
            return View(tariffEntity);
        }

        // GET: TariffEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tariffEntity = await _context.Tariffs
                .FirstOrDefaultAsync(m => m.TariffId == id);
            if (tariffEntity == null)
            {
                return NotFound();
            }

            return View(tariffEntity);
        }

        // POST: TariffEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tariffEntity = await _context.Tariffs.FindAsync(id);
            if (tariffEntity != null)
            {
                _context.Tariffs.Remove(tariffEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TariffEntityExists(int id)
        {
            return _context.Tariffs.Any(e => e.TariffId == id);
        }
    }
}
