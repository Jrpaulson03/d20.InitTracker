﻿using System;
using System.Collections.Generic;

namespace d20.InitTracker.Data.Models;

public partial class CombatantType
{
    public int CombatantTypeKey { get; set; }

    public string? CombatantTypeName { get; set; }

    public virtual ICollection<Combatant> Combatants { get; set; } = new List<Combatant>();
}
