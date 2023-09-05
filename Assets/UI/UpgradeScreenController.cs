using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScreenController : MonoBehaviour
{

    void OnEnable() {
        UpgradeComponent<String>.OnClicked += DoSomething;    
    }

    private void OnDisable() {
        UpgradeComponent<String>.OnClicked += DoSomething;    
    }

    private void DoSomething(string @string) {
        Debug.Log("Upgrade was clicked: " + @string);
    }

}
