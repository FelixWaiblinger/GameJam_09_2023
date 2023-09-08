using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public int Id;
    public string Name;
    public string Description;
    public Texture Icon;
    public float Cooldown;

    public abstract void Activate(Transform origin, Transform target);
}
