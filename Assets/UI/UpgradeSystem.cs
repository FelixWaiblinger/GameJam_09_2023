using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    private UpgradeTree _tree;

    [SerializeField] private SkillTreeSO skillTreeSO;

    // Start is called before the first frame update
    void Start()
    {
        _tree = new UpgradeTree(skillTreeSO.GetSkillTree());
    }

    private void DoSometing(string @string) {
        Debug.Log("Oh you want to upgrade: "+ @string);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
