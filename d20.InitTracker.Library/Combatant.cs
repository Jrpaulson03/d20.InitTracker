namespace d20.InitTracker.Library
{
    public abstract class Combatant
    {

        public int? EncounterKey { get; set; }
        public int? CombatantKey { get; set; }
        public int? Initiative { get; set; }
        public int? TurnOrder { get; set; }
        public string Name { get; set; } = null!;
        public int? Dex { get; set; }
        public int? InitiativeModifier { get; set; }
        public string? Dmnotes { get; set; }
        public string? ControlledBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }

        public abstract void RollInitiative();
        // Other common methods and properties for combatants can be added here


    }

}
