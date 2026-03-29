using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    // Drag your ability components into these slots in the Inspector
    public Ability ability1;
    public Ability ability2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ability1 != null) ability1.TryUse();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (ability2 != null) ability2.TryUse();
        }
    }
}
