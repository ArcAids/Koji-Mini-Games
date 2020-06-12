using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    public void Shoot(bool direction)
    {
        GetComponent<Rigidbody2D>().velocity = (direction?1:-1) *transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDie victim = collision.GetComponentInParent<IDie>();
        if (victim!=null)
        {
            victim.Dead(transform.position);
            GetComponent<Disabler>().Destroy();
        }
    }
}
