namespace d20.InitTracker.Web.Models
{
    public class EncounterCombatantDto
    {
        public int? EncounterKey { get; set; }
        public int? CombatantKey { get; set; }
        public int? Initiative { get; set; }
        public int? TurnOrder { get; set; }
    }
}
