using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectPool : ScriptableObject
{
    [SerializeField] GameObject prefab;
    [SerializeField] int prePooledNumber=15;
    [SerializeField] int maxPoolCapacity=15;
    [SerializeField] bool extendingAllowed = true;
    [SerializeField] bool recycling= false;
    [SerializeField] bool persists= false;
    Queue<IPooledObject> freed;
    List<IPooledObject> activeObjects;
    [System.NonSerialized]
    bool inited=false;
    public void Init()
    {
        if (persists)
        {
            if (inited) return;
            inited = true;
            hideFlags = HideFlags.DontUnloadUnusedAsset;
        }
        else
            hideFlags = HideFlags.None;

        freed = new Queue<IPooledObject>();
        activeObjects = new List<IPooledObject>();
        CreateNewPooledObject(prePooledNumber);
            
    }

    public GameObject InstantiatePooled()
    {
        IPooledObject pooledObject;

        if(freed.Count==0)
        {
            if (extendingAllowed)
            {
                if (activeObjects.Count+freed.Count < maxPoolCapacity)
                {
                    CreateNewPooledObject(1);
                    return GetFromFreedQueue()?.PooledGameObject;
                }
            }

            if(recycling && activeObjects.Count>0) {
                pooledObject= activeObjects[0];
                pooledObject.Destroy();
                pooledObject.Spawn();
                //Debug.Log("Recycling");
                return pooledObject.PooledGameObject;
            }
            else return null;
        }
        else
        {
            return GetFromFreedQueue()?.PooledGameObject;
        }
    }

    public T InstantiatePooled<T>()
    {
        GameObject temp = InstantiatePooled();
        if (temp == null)
            return default(T);
        else
            return temp.GetComponent<T>();
    }

    public void PutBackInPool(IPooledObject pooledObject)
    {
        if (pooledObject == null)
            return;
        if (activeObjects.Contains(pooledObject))
        {
            activeObjects.Remove(pooledObject);
            freed.Enqueue(pooledObject);
        }
    }

    public void RefreshPool()
    {
        while(activeObjects.Count>0)
        {
            activeObjects[0].Destroy();
        }

        foreach (var item in freed)
        {
            
            if (persists)
            {
                DontDestroyOnLoad(item.PooledGameObject);
            }
        }
    }
    public void DestroyPool()
    {
        while(freed.Count>0)
        {
            Destroy(freed.Dequeue().PooledGameObject);
        }
        foreach (var item in activeObjects)
        {
            Destroy(item.PooledGameObject);
        }
        freed.Clear();
        activeObjects.Clear();
        inited=false;
    }

    void CreateNewPooledObject(int numberOfObjects)
    {
        numberOfObjects = Mathf.Clamp(numberOfObjects,0, maxPoolCapacity);
        for (int i = 0; i < numberOfObjects; i++)
        {
            IPooledObject temp = Instantiate(prefab).GetComponent<IPooledObject>();
            temp.Init(this);
            temp.Destroy();
            freed.Enqueue(temp);
        }
    }
    IPooledObject GetFromFreedQueue()
    {
        IPooledObject pooledObject;

        pooledObject = freed.Dequeue();
        activeObjects.Add(pooledObject);
        pooledObject.Spawn();

        return pooledObject;
    }
}
