using UnityEngine;

public class CraftingItemTracker : MonoBehaviour
{
    public int requiredAmount = 1; // set in code in future
    public int filledAmount = 0;

    public bool CheckFilled()
    {
        return filledAmount == requiredAmount;
    }
}
