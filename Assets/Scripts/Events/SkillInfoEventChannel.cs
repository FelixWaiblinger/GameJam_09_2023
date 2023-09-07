using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SkillInfoEventChannel", menuName = "Events/SkillInfo Event Channel")]
public class SkillInfoEventChannel : ScriptableObject
{
    public UnityAction<SkillInfo> OnSkillInfoEventRaised;

    public void RaiseSkillInfoEvent(SkillInfo arg)
    {
        OnSkillInfoEventRaised?.Invoke(arg);
    }
}