using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace d20.InitTracker.Web.Controllers
{

    using global::d20.InitTracker.Data.Models;
    using global::d20.InitTracker.Web.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    namespace d20.InitTracker.Web.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class EncounterCombatantsController : ControllerBase
        {
            private readonly D20ProjectsContext _context;

            public EncounterCombatantsController(D20ProjectsContext context)
            {
                _context = context;
            }

            [HttpPost("Add")]
            public IActionResult AddCombatantToEncounter([FromBody] EncounterCombatantDto dto)
            {
                if (dto == null || !dto.EncounterKey.HasValue || !dto.CombatantKey.HasValue)
                {
                    return BadRequest("Invalid data.");
                }

                var encounterCombatant = _context.EncounterCombatants
                    .FirstOrDefault(ec => ec.EncounterKey == dto.EncounterKey && ec.CombatantKey == dto.CombatantKey);

                if (encounterCombatant != null)
                {
                    return BadRequest("Combatant is already in this encounter.");
                }

                encounterCombatant = new EncounterCombatant
                {
                    EncounterKey = dto.EncounterKey,
                    CombatantKey = dto.CombatantKey,
                    Initiative = dto.Initiative,
                    TurnOrder = dto.TurnOrder
                };

                _context.EncounterCombatants.Add(encounterCombatant);
                _context.SaveChanges();

                return Ok(encounterCombatant);
            }


            [HttpDelete("Remove")]
            public IActionResult RemoveCombatantFromEncounter([FromBody] EncounterCombatantDto dto)
            {
                if (dto == null || !dto.EncounterKey.HasValue || !dto.CombatantKey.HasValue)
                {
                    return BadRequest("Invalid data.");
                }

                var encounterCombatant = _context.EncounterCombatants
                    .FirstOrDefault(ec => ec.EncounterKey == dto.EncounterKey && ec.CombatantKey == dto.CombatantKey);

                if (encounterCombatant == null)
                {
                    return NotFound("Combatant not found in this encounter.");
                }

                _context.EncounterCombatants.Remove(encounterCombatant);
                _context.SaveChanges();

                return Ok();
            }



        }
    }

}
