using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPetFriendly.Models;

namespace WebPetFriendly.Controllers
{
    public class CitaMedicasController : Controller
    {
        private readonly VeterinariaDbContext _context;

        public CitaMedicasController(VeterinariaDbContext context)
        {
            _context = context;
        }

        // GET: CitaMedicas
        public async Task<IActionResult> Index()
        {
            var veterinariaDbContext = _context.CitaMedicas.Include(c => c.Mascota).Include(c => c.Medico);
            return View(await veterinariaDbContext.ToListAsync());
        }

        // GET: CitaMedicas/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citaMedica = await _context.CitaMedicas
                .Include(c => c.Mascota)
                .Include(c => c.Medico)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (citaMedica == null)
            {
                return NotFound();
            }

            return View(citaMedica);
        }

        // GET: CitaMedicas/Create
        public IActionResult Create()
        {
            ViewData["MascotaId"] = new SelectList(_context.Mascota, "IdMascota", "Nombre");
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "IdMedico", "Nombre");
            return View();
        }

        // POST: CitaMedicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCita,MedicoId,MascotaId,FechaCita,Sintomas,Diagnostico,Formula")] CitaMedica citaMedica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(citaMedica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MascotaId"] = new SelectList(_context.Mascota, "IdMascota", "Nombre", citaMedica.MascotaId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "IdMedico", "Nombre", citaMedica.MedicoId);
            return View(citaMedica);
        }

        // GET: CitaMedicas/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citaMedica = await _context.CitaMedicas.FindAsync(id);
            if (citaMedica == null)
            {
                return NotFound();
            }
            ViewData["MascotaId"] = new SelectList(_context.Mascota, "IdMascota", "Nombre", citaMedica.MascotaId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "IdMedico", "Nombre", citaMedica.MedicoId);
            return View(citaMedica);
        }

        // POST: CitaMedicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdCita,MedicoId,MascotaId,FechaCita,Sintomas,Diagnostico,Formula")] CitaMedica citaMedica)
        {
            if (id != citaMedica.IdCita)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citaMedica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaMedicaExists(citaMedica.IdCita))
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
            ViewData["MascotaId"] = new SelectList(_context.Mascota, "IdMascota", "Nombre", citaMedica.MascotaId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "IdMedico", "Nombre", citaMedica.MedicoId);
            return View(citaMedica);
        }

        // GET: CitaMedicas/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citaMedica = await _context.CitaMedicas
                .Include(c => c.Mascota)
                .Include(c => c.Medico)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (citaMedica == null)
            {
                return NotFound();
            }

            return View(citaMedica);
        }

        // POST: CitaMedicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var citaMedica = await _context.CitaMedicas.FindAsync(id);
            if (citaMedica != null)
            {
                _context.CitaMedicas.Remove(citaMedica);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaMedicaExists(long id)
        {
            return _context.CitaMedicas.Any(e => e.IdCita == id);
        }
    }
}
