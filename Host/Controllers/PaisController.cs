using Microsoft.AspNetCore.Mvc;
using Dal;
using Dal.UnitOfWorks;

namespace WebHost.Controllers
{
    public class PaisController : Controller
    {
        private readonly IUnitOfWork _context;

        public PaisController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: Pais
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaisRepository.GetAsync());
        }

        // GET: Pais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pais = await _context.PaisRepository
                .GetAsync(new object[] {id});
            if (pais == null)
            {
                return NotFound();
            }

            return View(pais);
        }

        // GET: Pais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,CodigoPais")] Pais pais)
        {
            if (ModelState.IsValid)
            {
                _context.PaisRepository.Add(pais);
                await _context.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pais);
        }

        // GET: Pais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pais = await _context.PaisRepository.GetAsync(new object[] { id });
            if (pais == null)
            {
                return NotFound();
            }
            return View(pais);
        }

        // POST: Pais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,CodigoPais")] Pais pais)
        {
            if (id != pais.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.PaisRepository.Update(pais);
                    await _context.SaveAsync();
                }
                catch (Exception ex)
                {
                    if (!PaisExists(pais.Id))
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
            return View(pais);
        }

        // GET: Pais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pais = await _context.PaisRepository.GetAsync(new object[] { id });
            if (pais == null)
            {
                return NotFound();
            }

            return View(pais);
        }

        // POST: Pais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pais = await _context.PaisRepository.GetAsync(new object[] { id });
            if (pais != null)
            {
                _context.PaisRepository.Delete(pais);
            }

            await _context.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaisExists(int id)
        {
            var pais =  _context.PaisRepository.Get(new object[] { id });
            return pais is null ? false : true;
        }
    }
}
