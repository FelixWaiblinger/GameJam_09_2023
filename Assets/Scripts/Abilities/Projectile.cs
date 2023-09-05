using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform _target;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, 10 * Time.deltaTime);
    }

    public void Init(Transform target)
    {
        _target = target;
    }
}
