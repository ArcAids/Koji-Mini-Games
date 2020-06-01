using UnityEngine;
using System;

public class Handle
{
    public delegate void Action();

    private Action releaseFn;

    public Handle(Action releaseFn)
    {
        this.releaseFn = releaseFn;
    }

    public bool IsReleased
    {
        get
        {
            return this.releaseFn == null;
        }
    }

    public bool Release()
    {
        Debug.Log("Release Handle");

        if (this.releaseFn == null)
        {
            return false;
        }

        try
        {
            this.releaseFn.Invoke();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }

        this.releaseFn = null;

        return true;
    }
}
