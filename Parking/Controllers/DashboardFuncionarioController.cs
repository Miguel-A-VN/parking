using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking.Data;
using Parking.Models.Entities;
using Parking.ViewModel;

namespace Parking.Controllers
{
    [Authorize(Roles = "Staff")]
    public class DashboardFuncionarioController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardFuncionarioController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserList()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // GET: formulario para crear usuario
        [Authorize(Roles = "Staff")]
        public IActionResult CreateUser()
        {
            return View();
        }


        // POST: crear usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> CreateUser(RegisterAprendizViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // Evitar duplicados
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                ModelState.AddModelError(string.Empty, "Email ya registrado.");
                return View(model);
            }

            // Crear usuario como Aprendiz
            var user = new UserEntity
            {
                FullName = model.FullName,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = "Apprentice",
                RegisteredAt = DateTime.UtcNow
            };

            _context.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("UserList");
        }


    }
}
