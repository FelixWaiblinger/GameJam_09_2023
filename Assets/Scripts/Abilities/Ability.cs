using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public float Cooldown;

    public abstract void Activate(Transform origin, Transform target);
}
