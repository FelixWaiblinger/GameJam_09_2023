using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "StringEventChannel", menuName = "Events/String Event Channel")]
public class StringEventChannel : ScriptableObject
{
    public UnityAction<string> OnStringEventRaised;

    public void RaiseStringEvent(string arg)
    {
        OnStringEventRaised?.Invoke(arg);
    }
}
