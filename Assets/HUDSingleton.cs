using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDSingleton : MonoBehaviour
{
    private static HUDSingleton Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
}
