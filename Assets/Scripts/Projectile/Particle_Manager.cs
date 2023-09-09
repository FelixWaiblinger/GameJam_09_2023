using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Manager : MonoBehaviour {

    public float _speed = 0f;

    public bool ShouldUseSpeedvalue = false;

    public float _customGravityScale;

    private Rigidbody _rb;

    public GameObject boom;
    public GameObject[] Detachables;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.None;
    }
    void FixedUpdate() {

        _rb.useGravity = !ShouldUseSpeedvalue;

        if (ShouldUseSpeedvalue && _speed != 0) {
            _rb.velocity = transform.forward * _speed;
        }

        if (_rb.useGravity) {
            Vector3 customGravity = Physics.gravity * _customGravityScale;
            _rb.AddForce(customGravity, ForceMode.Acceleration);

        }
    }

    private void OnCollisionEnter(Collision collision) {

        _rb.constraints = RigidbodyConstraints.FreezeAll;

        Vector3 currentPosition = this.transform.position;

        //Destroy projectile on collision
        Instantiate(boom, currentPosition, Quaternion.Euler(0,0,0));
        Destroy(gameObject);


    }
}
