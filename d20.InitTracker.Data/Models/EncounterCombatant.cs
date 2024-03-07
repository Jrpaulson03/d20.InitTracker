using System;
using System.Collections.Generic;

namespace d20.InitTracker.Data.Models;

public partial class EncounterCombatant
{
    public int EncounterCombatantKey { get; set; }

    public int? EncounterKey { get; set; }

    public int? CombatantKey { get; set; }

    public int? Initiative { get; set; }

    public int? TurnOrder { get; set; }

    public virtual Combatant? CombatantKeyNavigation { get; set; }

    public virtual Encounter? EncounterKeyNavigation { get; set; }
}
