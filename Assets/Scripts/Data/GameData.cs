using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game/Game Data")]
public class GameData : ScriptableObject
{
    public Ability Attack;
    public Ability Primary;
    public Ability Secondary;
}
