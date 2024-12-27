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
    public class DepartamentoesController : Controller
    {
        private readonly IUnitOfWork _context;

        public DepartamentoesController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: Departamentoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DepartamentoRepository.GetAsync(null,null,d => d.IdPaisNavigation);
            return View(await applicationDbContext);
        }

        // GET: Departamentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var departamentos = await _context.DepartamentoRepository.GetAsync(d => d.IdDepartamento == id, null, d => d.IdPaisNavigation);
            var departamento = departamentos.FirstOrDefault();
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // GET: Departamentoes/Create
        public async Task<IActionResult> Create()
        {
            var paises = await _context.PaisRepository.GetAsync();
            ViewData["IdPais"] = new SelectList(paises, "Id", "Nombre");
            return View();
        }

        // POST: Departamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPais,IdDepartamento,NombreDepartamento,CodigoDepartamento")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _context.DepartamentoRepository.Add(departamento);
                await _context.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            var paises = await _context.PaisRepository.GetAsync();
            ViewData["IdPais"] = new SelectList(paises, "Id", "CodigoPais", departamento.IdPais);
            return View(departamento);
        }

        // GET: Departamentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var departamentos = await _context.DepartamentoRepository.GetAsync(d => d.IdDepartamento == id,null, d => d.IdPaisNavigation);
            var departamento = departamentos.FirstOrDefault();
            if (departamento == null)
            {
                return NotFound();
            }
            var paises = await _context.PaisRepository.GetAsync();
            ViewData["IdPais"] = new SelectList(paises, "Id", "CodigoPais", departamento.IdPais);
            return View(departamento);
        }

        // POST: Departamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPais,IdDepartamento,NombreDepartamento,CodigoDepartamento")] Departamento departamento)
        {
            if (id != departamento.IdDepartamento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.DepartamentoRepository.Update(departamento);
                    await _context.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentoExists(departamento.IdDepartamento))
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
            var paises = await _context.PaisRepository.GetAsync();
            ViewData["IdPais"] = new SelectList(paises, "Id", "CodigoPais", departamento.IdPais);
            return View(departamento);
        }

        // GET: Departamentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamentos = await _context.DepartamentoRepository.GetAsync(d => d.IdDepartamento == id, null, d => d.IdPaisNavigation);
            var departamento = departamentos.FirstOrDefault();
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // POST: Departamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departamentos = await _context.DepartamentoRepository.GetAsync(d => d.IdDepartamento == id);
            var departamento = departamentos.FirstOrDefault();
            if (departamento != null)
            {
                _context.DepartamentoRepository.Delete(departamento);
            }

            await _context.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartamentoExists(int id)
        {
            var departamentos = _context.DepartamentoRepository.Get(d => d.IdPais == id);
            return departamentos.Any();
        }
    }
}
