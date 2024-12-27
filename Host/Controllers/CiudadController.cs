using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dal;
using Dal.Context;
using Dal.UnitOfWorks;

namespace WebHost.Controllers
{
    public class CiudadController : Controller
    {
        private readonly IUnitOfWork _context;

        public CiudadController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: Ciudad
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CiudadRepository.GetAsync(null,null,c => c.Departamento);
            return View(await applicationDbContext);
        }

        // GET: Ciudad/Details/5
        public async Task<IActionResult> Details(int? idPais,int? idDepartamento,int? idCiudad)
        {
            if (idPais is null || idDepartamento == null || idCiudad == null)
            {
                return NotFound();
            }

            var ciudades = await _context.CiudadRepository.GetAsync(c => c.IdCiudad == idCiudad && c.IdDepartamento == idDepartamento && c.IdPais == idPais, null, c => c.Departamento);
            var ciudad = ciudades.FirstOrDefault();
            if (ciudad == null)
            {
                return NotFound();
            }

            return View(ciudad);
        }

        // GET: Ciudad/Create
        public async Task<IActionResult> Create()
        {
            var departamentos = await _context.DepartamentoRepository.GetAsync(null,null, c => c.IdPaisNavigation);
            var paises = departamentos.Select(x => x.IdPaisNavigation).Distinct().ToList();
            ViewData["IdPais"] = new SelectList(paises, "Id", "Nombre");
            ViewData["IdDepartamento"] = new SelectList(departamentos.ToList(), "IdDepartamento", "NombreDepartamento");

            return View();
        }

        // POST: Ciudad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPais,IdDepartamento,IdCiudad,NombreCiudad,CodigoCiudad")] Ciudad ciudad)
        {
            if (ModelState.IsValid)
            {
                _context.CiudadRepository.Add(ciudad);
                await _context.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            var departamentos = await _context.DepartamentoRepository.GetAsync(null, null, c => c.IdPaisNavigation);
            var paises = departamentos.Select(x => x.IdPaisNavigation).Distinct().ToList();
            ViewData["IdPais"] = new SelectList(paises, "Id", "Nombre");
            ViewData["IdDepartamento"] = new SelectList(departamentos.ToList(), "IdDepartamento", "NombreDepartamento");
            return View(ciudad);
        }

        // GET: Ciudad/Edit/5
        public async Task<IActionResult> Edit(int? idPais, int? idDepartamento, int? idCiudad)
        {
            if (idPais is null || idDepartamento == null || idCiudad == null)
            {
                return NotFound();
            }

            var ciudades = await _context.CiudadRepository.GetAsync(c => c.IdCiudad == idCiudad && c.IdDepartamento == idDepartamento && c.IdPais == idPais, null, c => c.Departamento);
            var ciudad = ciudades.FirstOrDefault();
            if (ciudad == null)
            {
                return NotFound();
            }
            var departamentos = await _context.DepartamentoRepository.GetAsync();
            ViewData["IdPais"] = new SelectList(departamentos, "IdPais", "CodigoDepartamento", ciudad.IdPais);
            return View(ciudad);
        }

        // POST: Ciudad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPais,IdDepartamento,IdCiudad,NombreCiudad,CodigoCiudad")] Ciudad ciudad)
        {
            if (id != ciudad.IdPais)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.CiudadRepository.Update(ciudad);
                    await _context.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CiudadExists(ciudad.IdPais))
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
            var departamentos = await _context.DepartamentoRepository.GetAsync();
            ViewData["IdPais"] = new SelectList(departamentos, "IdPais", "CodigoDepartamento", ciudad.IdPais);
            return View(ciudad);
        }

        // GET: Ciudad/Delete/5
        public async Task<IActionResult> Delete(int? idPais, int? idDepartamento, int? idCiudad)
        {
            if (idPais is null || idDepartamento == null || idCiudad == null)
            {
                return NotFound();
            }

            var ciudades = await _context.CiudadRepository.GetAsync(c => c.IdCiudad == idCiudad && c.IdDepartamento == idDepartamento && c.IdPais == idPais, null, c => c.Departamento);
            var ciudad = ciudades.FirstOrDefault();
            if (ciudad == null)
            {
                return NotFound();
            }

            return View(ciudad);
        }

        // POST: Ciudad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? idPais, int? idDepartamento, int? idCiudad)
        {
            if (idPais is null || idDepartamento == null || idCiudad == null)
            {
                return NotFound();
            }

            var ciudades = await _context.CiudadRepository.GetAsync(c => c.IdCiudad == idCiudad && c.IdDepartamento == idDepartamento && c.IdPais == idPais, null, c => c.Departamento);
            var ciudad = ciudades?.FirstOrDefault();
            if (ciudad != null)
            {
                _context.CiudadRepository.Delete(ciudad);
            }

            await _context.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CiudadExists(int id)
        {
            var ciudades = _context.CiudadRepository.Get( c => c.IdCiudad == id);
            return ciudades.Any();
        }
        public IActionResult GetDepartamentos(int idPais)
        {
            // Obtén los departamentos del país seleccionado
            var departamentos =  _context.DepartamentoRepository
                                          .Get(d => d.IdPais == idPais)
                                          .ToList();

            // Devuelve los departamentos como JSON
            var result = departamentos.Select(d => new { id = d.IdDepartamento, nombre = d.NombreDepartamento }).ToList();
            return Json(result);
        }
    }
}
