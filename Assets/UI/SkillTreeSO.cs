using UnityEngine;

[CreateAssetMenu(fileName = "new Skilltree", menuName = "whatever")]
public class SkillTreeSO : ScriptableObject
{
    [SerializeField] private SkillNodeSO _root;

    public SkillNodeSO GetSkillTree() {
        return _root;
    }

}

