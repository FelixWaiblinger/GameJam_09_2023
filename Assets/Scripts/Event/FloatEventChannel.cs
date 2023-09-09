using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FloatEventChannel", menuName = "Events/Float Event Channel")]
public class FloatEventChannel : ScriptableObject
{
    public UnityAction<float> OnFloatEventRaised;

    public void RaiseFloatEvent(float arg)
    {
        OnFloatEventRaised?.Invoke(arg);
    }
}