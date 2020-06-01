using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour, IPooledObject
{
    public ObjectPool Group { get; private set; }

    public GameObject PooledGameObject => gameObject;

    public void Destroy()
    {
        gameObject.SetActive(false);
        transform.parent = null;
        Group.PutBackInPool(this);
    }

    public void Init(ObjectPool group)
    {
        Group = group;
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }
}

public interface IPooledObject
{
    ObjectPool Group { get; }
    void Init(ObjectPool group);
    GameObject PooledGameObject { get; }
    void Spawn();
    void Destroy();
}