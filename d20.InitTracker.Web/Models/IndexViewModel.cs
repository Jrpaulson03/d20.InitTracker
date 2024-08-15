using d20.InitTracker.Data.Models;

namespace d20.InitTracker.Web.Models
{
    public class IndexViewModel
    {
       public List<Encounter> encounters;
       public List<Combatant> combatants;

        public IndexViewModel()
        {
            encounters = new List<Encounter>();
            combatants = new List<Combatant>();
        }
    }
}
