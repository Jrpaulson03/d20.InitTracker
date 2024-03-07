using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using d20.InitTracker.Data.Models;

namespace d20.InitTracker.Web.Controllers
{
    public class CombatantTypesController : Controller
    {
        private readonly D20ProjectsContext _context;

        public CombatantTypesController(D20ProjectsContext context)
        {
            _context = context;
        }

        // GET: CombatantTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CombatantTypes.ToListAsync());
        }

        // GET: CombatantTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var combatantType = await _context.CombatantTypes
                .FirstOrDefaultAsync(m => m.CombatantTypeKey == id);
            if (combatantType == null)
            {
                return NotFound();
            }

            return View(combatantType);
        }

        // GET: CombatantTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CombatantTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CombatantTypeKey,CombatantTypeName")] CombatantType combatantType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(combatantType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(combatantType);
        }

        // GET: CombatantTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var combatantType = await _context.CombatantTypes.FindAsync(id);
            if (combatantType == null)
            {
                return NotFound();
            }
            return View(combatantType);
        }

        // POST: CombatantTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CombatantTypeKey,CombatantTypeName")] CombatantType combatantType)
        {
            if (id != combatantType.CombatantTypeKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(combatantType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CombatantTypeExists(combatantType.CombatantTypeKey))
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
            return View(combatantType);
        }

        // GET: CombatantTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var combatantType = await _context.CombatantTypes
                .FirstOrDefaultAsync(m => m.CombatantTypeKey == id);
            if (combatantType == null)
            {
                return NotFound();
            }

            return View(combatantType);
        }

        // POST: CombatantTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var combatantType = await _context.CombatantTypes.FindAsync(id);
            if (combatantType != null)
            {
                _context.CombatantTypes.Remove(combatantType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CombatantTypeExists(int id)
        {
            return _context.CombatantTypes.Any(e => e.CombatantTypeKey == id);
        }
    }
}
