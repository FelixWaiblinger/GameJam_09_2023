using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour {
    // providing a gravity scale per object

    public float gravityScale = 1.0f;

    //public static float globalGravity = -9.81f;

    Rigidbody _rb;

    void OnEnable() {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }

    void FixedUpdate() {
        
        _rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
    }
}
