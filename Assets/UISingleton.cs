using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISingleton : MonoBehaviour
{
    private static UISingleton Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
}
