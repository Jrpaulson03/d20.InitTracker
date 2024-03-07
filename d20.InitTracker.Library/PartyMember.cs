using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d20.InitTracker.Library
{
    public class PartyMember : Combatant
    {
        public string PlayedBy { get; set; } = null!;

        public override void RollInitiative()
        {
            Random random = new Random();
            int roll = random.Next(1, 21); // Roll a d20 (1-20)
            Initiative = roll + (InitiativeModifier ?? 0);
        }
    }

}
