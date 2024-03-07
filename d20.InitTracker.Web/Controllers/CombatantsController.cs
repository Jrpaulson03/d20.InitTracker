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
    public class CombatantsController : Controller
    {
        private readonly D20ProjectsContext _context;

        public CombatantsController(D20ProjectsContext context)
        {
            _context = context;
        }

        // GET: Combatants
        public async Task<IActionResult> Index()
        {
            return View(await _context.Combatants.ToListAsync());
        }

        // GET: Combatants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var combatant = await _context.Combatants
                .FirstOrDefaultAsync(m => m.CombatantKey == id);
            if (combatant == null)
            {
                return NotFound();
            }

            return View(combatant);
        }

        // GET: Combatants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Combatants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CombatantKey,CombatantType,Name,Dex,InitiativeModifier,Dmnotes,ControlledBy,CreatedDateTime")] Combatant combatant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(combatant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(combatant);
        }

        // GET: Combatants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var combatant = await _context.Combatants.FindAsync(id);
            if (combatant == null)
            {
                return NotFound();
            }
            return View(combatant);
        }

        // POST: Combatants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CombatantKey,CombatantType,Name,Dex,InitiativeModifier,Dmnotes,ControlledBy,CreatedDateTime")] Combatant combatant)
        {
            if (id != combatant.CombatantKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(combatant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CombatantExists(combatant.CombatantKey))
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
            return View(combatant);
        }

        // GET: Combatants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var combatant = await _context.Combatants
                .FirstOrDefaultAsync(m => m.CombatantKey == id);
            if (combatant == null)
            {
                return NotFound();
            }

            return View(combatant);
        }

        // POST: Combatants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var combatant = await _context.Combatants.FindAsync(id);
            if (combatant != null)
            {
                _context.Combatants.Remove(combatant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CombatantExists(int id)
        {
            return _context.Combatants.Any(e => e.CombatantKey == id);
        }
    }
}
