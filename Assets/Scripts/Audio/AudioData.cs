using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "Audio/Audio Data")]
public class AudioData : ScriptableObject
{
    public string Name;
    [Range(0f, 1f)] public float Volume;
    [Range(0.1f, 3f)] public float Pitch;
    public bool Loop;
    public AudioClip Clip;
}
