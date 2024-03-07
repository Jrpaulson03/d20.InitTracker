using System;
using System.Collections.Generic;

namespace d20.InitTracker.Data.Models;

public partial class Combatant
{
    public int CombatantKey { get; set; }

    public int? CombatantType { get; set; }

    public string Name { get; set; } = null!;

    public int? Dex { get; set; }

    public int? InitiativeModifier { get; set; }

    public string? Dmnotes { get; set; }

    public string? ControlledBy { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public virtual ICollection<EncounterCombatant> EncounterCombatants { get; set; } = new List<EncounterCombatant>();
}
