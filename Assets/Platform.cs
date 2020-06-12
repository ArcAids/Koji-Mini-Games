using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour, IPooledObject
{
    public ObjectPool Group { get; private set; }

    private static Vector3 originalScale;

    public GameObject PooledGameObject => gameObject;

    public float RandomizeZScale(float difficultyFactor)
    {
        float randomScale = Random.Range(2f-difficultyFactor, 4f-difficultyFactor);
        transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y, randomScale);
        return randomScale;
    }
    public float RandomizeXScale(float difficultyFactor)
    {
        float randomScale = Random.Range(2f - difficultyFactor, 4f - difficultyFactor);
        transform.localScale = new Vector3(randomScale, transform.localScale.y, transform.localScale.z);
        return randomScale;
    }

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
        originalScale=transform.localScale;
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
        transform.localScale = originalScale;
    }

    public void SpawnAnimation()
    {
        Vector3 position = transform.localPosition;
        transform.position= new Vector3( transform.localPosition.x, transform.localPosition.y + 5,transform.localPosition.z);
        MoveToPosition(position,null);
    }

    IEnumerator InitiateFall()
    {
        yield return new WaitForSeconds(0.5f);
        MoveToPosition(new Vector3(transform.localPosition.x, transform.localPosition.y - 5,transform.localPosition.z),Destroy);
    }

    void MoveToPosition(Vector3 position, TweenCallback onComplete)
    {
        transform.DOLocalMove(position, 20).SetSpeedBased().SetEase(Ease.InSine).onComplete=onComplete;
    }
}
