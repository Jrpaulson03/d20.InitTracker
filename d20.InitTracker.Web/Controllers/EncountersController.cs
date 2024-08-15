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
    public class EncountersController : Controller
    {
        private readonly D20ProjectsContext _context;

        public EncountersController(D20ProjectsContext context)
        {
            _context = context;
        }

        // GET: Encounters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Encounters.ToListAsync());
        }

        // GET: Encounters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encounter = await _context.Encounters
                .FirstOrDefaultAsync(m => m.EncounterKey == id);
            if (encounter == null)
            {
                return NotFound();
            }

            return View(encounter);
        }

        // GET: Encounters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Encounters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EncounterKey,EncounterName,EncounterStartDate")] Encounter encounter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(encounter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(encounter);
        }

        // GET: Encounters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encounter = _context.Encounters
                 .Include(e => e.EncounterCombatants)
                 .ThenInclude(ec => ec.CombatantKeyNavigation)
                 .FirstOrDefault(e => e.EncounterKey == id);

            ViewBag.AllCombatants = _context.Combatants.ToList();


            if (encounter == null)
            {
                return NotFound();
            }
            return View(encounter);
        }

        // POST: Encounters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EncounterKey,EncounterName,EncounterStartDate")] Encounter encounter)
        {
            if (id != encounter.EncounterKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(encounter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncounterExists(encounter.EncounterKey))
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
            return View(encounter);
        }

        // GET: Encounters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encounter = await _context.Encounters
                .FirstOrDefaultAsync(m => m.EncounterKey == id);
            if (encounter == null)
            {
                return NotFound();
            }

            return View(encounter);
        }

        // POST: Encounters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var encounter = await _context.Encounters.FindAsync(id);
            if (encounter != null)
            {
                _context.Encounters.Remove(encounter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EncounterExists(int id)
        {
            return _context.Encounters.Any(e => e.EncounterKey == id);
        }

        [HttpGet]
        [Route("api/Encounters/{encounterId}/Combatants")]
        public IActionResult GetCombatantsInEncounter(int encounterId)
        {
            var combatants = _context.EncounterCombatants
                .Where(ec => ec.EncounterKey == encounterId)
                .OrderBy(ec => ec.Initiative)
                .Select(ec => new
                {
                    ec.CombatantKey,
                    ec.CombatantKeyNavigation.Name,
                    ec.Initiative
                })
                .ToList();

            return Ok(combatants);
        }


    }
}
