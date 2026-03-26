using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public float cooldown = 5f;
    private float lastUsedTime;

    public bool CanUse()
    {
        return Time.time >= lastUsedTime + cooldown;
    }

    public void TryUse()
    {
        if (CanUse())
        {
            Activate();
            lastUsedTime = Time.time;
        }
    }

    protected abstract void Activate();
}