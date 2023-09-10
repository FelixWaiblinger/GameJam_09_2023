using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTonTest : MonoBehaviour
{
    private static SingleTonTest Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
}
