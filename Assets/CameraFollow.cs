using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;

    Vector3 offset;
    // Update is called once per frame

    private void Start()
    {
        offset = transform.position-target.position;
    }
    void FixedUpdate()
    {
        transform.position=target.position+offset;
    }
}
