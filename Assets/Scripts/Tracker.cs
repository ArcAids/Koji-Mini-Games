using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField]
    Transform target;

    void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(target.position.x, target.position.y, -10),
            0.5f
         );
    }
}
