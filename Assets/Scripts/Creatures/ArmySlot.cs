public struct ArmySlot
{
    public string creature;
    public int amount;

    public ArmySlot(string creatureName, int creatureAmount)
    {
        creature = creatureName;
        amount = creatureAmount;
    }
}