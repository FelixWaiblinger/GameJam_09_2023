using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControlSingleton : MonoBehaviour
{
    private static UIControlSingleton Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
}
