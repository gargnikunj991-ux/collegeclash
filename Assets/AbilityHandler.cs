using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    // These slots will hold your PowerPush or SlowField scripts
    public Ability ability1;
    public Ability ability2;

    public KeyCode key1 = KeyCode.E;
    public KeyCode key2 = KeyCode.Q;

    void Update()
    {
        if (Input.GetKeyDown(key1) && ability1 != null)
        {
            ability1.TryUse();
        }

        if (Input.GetKeyDown(key2) && ability2 != null)
        {
            ability2.TryUse();
        }
    }
}