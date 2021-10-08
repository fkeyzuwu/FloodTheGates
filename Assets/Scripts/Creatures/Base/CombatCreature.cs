using UnityEngine;
using UnityEngine.AI;

public class CombatCreature
{
    public NavMeshAgent agent;
    public int slotIndex;
    public KeyCode selectKey;

    public CombatCreature(GameObject creature, int creatureSlotIndex)
    {
        agent = creature.GetComponent<NavMeshAgent>();
        slotIndex = creatureSlotIndex;
        selectKey = GetKeyByIndex(slotIndex);
    }
    
    private KeyCode GetKeyByIndex(int index)
    {
        switch (index)
        {
            case 0:
                return KeyCode.Q;
            case 1:
                return KeyCode.W;
            case 2:
                return KeyCode.E;
            case 3:
                return KeyCode.R;
            case 4:
                return KeyCode.A;
            case 5:
                return KeyCode.S;
            case 6:
                return KeyCode.D;
            default:
                Debug.Log("WOohoaohaoh");
                return KeyCode.None;
        }
    }
}