using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    private static EventSystem _instance;

    private void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
}
