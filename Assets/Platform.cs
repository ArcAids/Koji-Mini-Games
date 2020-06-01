using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour, IPooledObject
{
    public ObjectPool Group { get; private set; }

    public GameObject PooledGameObject => gameObject;

    public void Destroy()
    {
        gameObject.SetActive(false);
        Group?.PutBackInPool(this);
    }

    public void Fall()
    {
        StartCoroutine(InitiateFall());
    }

    public void Init(ObjectPool group)
    {
        Group = group;
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }

    IEnumerator InitiateFall()
    {
        yield return new WaitForSeconds(0.5f);
        MoveToPosition(new Vector3(transform.position.x, transform.position.y - 5,transform.position.z));
    }

    void MoveToPosition(Vector3 position)
    {
        transform.DOLocalMove(position, 20).SetSpeedBased().SetEase(Ease.InSine).onComplete=Destroy;
    }
}
