using System;
using System.Collections.Generic;

namespace d20.InitTracker.Data.Models;

public partial class Encounter
{
    public int EncounterKey { get; set; }

    public string? EncounterName { get; set; }

    public DateTime? EncounterStartDate { get; set; }

    public virtual ICollection<EncounterCombatant> EncounterCombatants { get; set; } = new List<EncounterCombatant>();
}
