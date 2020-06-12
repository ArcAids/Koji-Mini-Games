using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] float blastPower;
    Rigidbody2D rigid;
    Disabler disabler;
    public void Init()
    {
        TryGetComponent(out rigid);
        TryGetComponent(out disabler);
    }

    public void ResetRigid()
    {
        rigid.bodyType = RigidbodyType2D.Kinematic;
    }

    public void Flip()
    {
        if (transform.position.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }

    public void BlastAway()
    {
        rigid.bodyType = RigidbodyType2D.Dynamic;
        rigid.AddForce ((transform.up-transform.right).normalized * blastPower,ForceMode2D.Impulse);
        rigid.AddTorque(blastPower* Random.Range(10,50));
        disabler.DisableWithDelay();
    }
}
