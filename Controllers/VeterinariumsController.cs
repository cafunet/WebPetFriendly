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
    public class VeterinariumsController : Controller
    {
        private readonly VeterinariaDbContext _context;

        public VeterinariumsController(VeterinariaDbContext context)
        {
            _context = context;
        }

        // GET: Veterinariums
        public async Task<IActionResult> Index()
        {
            //native /expresion
            /*
            from
            where
            select
             */
            var veterinarias = (from veterinary in _context.Veterinaria
                                //where veterinary.Nombre == "Paco"
                                //where veterinary.Telefono.Contains("31221")
                                where veterinary.Nit.StartsWith("12")
                                select veterinary);

            //metodos extension
            //expresiones lambda
            var veterinariasEXT = _context.Veterinaria
                .Where(veterinary => veterinary.Telefono.Contains("31221"))
                .Select(veterinary => veterinary);


            return View(veterinarias);
        }

        // GET: Veterinariums/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinarium = await _context.Veterinaria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veterinarium == null)
            {
                return NotFound();
            }

            return View(veterinarium);
        }

        // GET: Veterinariums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Veterinariums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nit,Nombre,Direccion,Telefono,Email,Empleados,FechaFundacion")] Veterinarium veterinarium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(veterinarium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(veterinarium);
        }

        // GET: Veterinariums/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinarium = await _context.Veterinaria.FindAsync(id);
            if (veterinarium == null)
            {
                return NotFound();
            }
            return View(veterinarium);
        }

        // POST: Veterinariums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nit,Nombre,Direccion,Telefono,Email,Empleados,FechaFundacion")] Veterinarium veterinarium)
        {
            if (id != veterinarium.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veterinarium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinariumExists(veterinarium.Id))
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
            return View(veterinarium);
        }

        // GET: Veterinariums/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinarium = await _context.Veterinaria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veterinarium == null)
            {
                return NotFound();
            }

            return View(veterinarium);
        }

        // POST: Veterinariums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var veterinarium = await _context.Veterinaria.FindAsync(id);
            if (veterinarium != null)
            {
                _context.Veterinaria.Remove(veterinarium);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinariumExists(long id)
        {
            return _context.Veterinaria.Any(e => e.Id == id);
        }
    }
}
