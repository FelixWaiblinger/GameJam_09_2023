using UnityEngine;

public class SkillInfo
{
    public float CurrentCoolDown;
    public float MaxCooldown;

    public int Identifier;
    public string Name;
    public Texture UIIcon;

    public SkillInfo() { }

    public SkillInfo(float currentCD, Ability skill)
    {
        CurrentCoolDown = currentCD;
        MaxCooldown = skill.Cooldown;
        Identifier = skill.Id;
        Name = skill.Name;
        UIIcon = skill.Icon;
    }

}