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
    public class MascotumsController : Controller
    {
        private readonly VeterinariaDbContext _context;

        public MascotumsController(VeterinariaDbContext context)
        {
            _context = context;
        }

        // GET: Mascotums
        public async Task<IActionResult> Index()
        {

            //native /expresion
            /*
            from
            where
            select
            join
             */
            var mascotas = from mascotica in _context.Mascota
                            where mascotica.Propietario == null
                            select mascotica;
            //consulta para mascotas que tienen mas de 1 cita con where
            var citaMascota = from mascota in _context.Mascota
                           where mascota.CitaMedicas.Count > 1 
                           select mascota;

            //consulta para mascotas que tienen mas de 1 cita con join
            var idsMascota = from mascota in _context.Mascota
                              join cita in _context.CitaMedicas on mascota.IdMascota equals cita.MascotaId
                              group cita by cita.MascotaId into grupoCita
                              where grupoCita.Count() > 1
                              select grupoCita.Key;

            var citaMascota1 = from mascota in _context.Mascota
                              where idsMascota.Contains(mascota.IdMascota)
                              select mascota;
            //metodos extension
            //expresiones lambda
            //var veterinariasEXT = _context.Veterinaria
            //    .Where(veterinary => veterinary.Telefono.Contains("31221"))
            //    .Select(veterinary => veterinary);

            return View(citaMascota1);
        }

        // GET: Mascotums/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascotum = await _context.Mascota
                .FirstOrDefaultAsync(m => m.IdMascota == id);
            if (mascotum == null)
            {
                return NotFound();
            }

            return View(mascotum);
        }

        // GET: Mascotums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mascotums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMascota,Nombre,Especie,Raza,Color,FechaNacimiento,Propietario")] Mascotum mascotum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mascotum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mascotum);
        }

        // GET: Mascotums/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascotum = await _context.Mascota.FindAsync(id);
            if (mascotum == null)
            {
                return NotFound();
            }
            return View(mascotum);
        }

        // POST: Mascotums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdMascota,Nombre,Especie,Raza,Color,FechaNacimiento,Propietario")] Mascotum mascotum)
        {
            if (id != mascotum.IdMascota)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mascotum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MascotumExists(mascotum.IdMascota))
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
            return View(mascotum);
        }

        // GET: Mascotums/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascotum = await _context.Mascota
                .FirstOrDefaultAsync(m => m.IdMascota == id);
            if (mascotum == null)
            {
                return NotFound();
            }

            return View(mascotum);
        }

        // POST: Mascotums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var mascotum = await _context.Mascota.FindAsync(id);
            if (mascotum != null)
            {
                _context.Mascota.Remove(mascotum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MascotumExists(long id)
        {
            return _context.Mascota.Any(e => e.IdMascota == id);
        }
    }
}
