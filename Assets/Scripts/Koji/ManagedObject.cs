using UnityEngine;
using System.Collections.Generic;

public abstract class ManagedObject : MonoBehaviour
{
    protected List<Handle> Handles { get; private set; }

    public ManagedObject()
    {
        Handles = new List<Handle>();
    }
    
    protected virtual void Stop()
    {
        Handles.ForEach(handle =>
        {
            handle.Release();
        });
        Handles.Clear();
    }
}
