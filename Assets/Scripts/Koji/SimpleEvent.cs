using System.Collections.Generic;
using UnityEngine;

public class SimpleEvent<T>
{
    public delegate void SimpleEventHandler(T newValue);

    private List<SimpleEventHandler> handlers;
    private T lastEvent;

    public Handle Subscribe(SimpleEventHandler handler, bool includeLast = false)
    {
        if (this.handlers == null)
        {
            this.handlers = new List<SimpleEventHandler>();
        }

        this.handlers.Add(handler);

        if (this.lastEvent != null && includeLast)
        {
            handler.Invoke(this.lastEvent);
        }

        return new Handle(() => this.Unsubscribe(handler));
    }

    public void Emit(T evt)
    {
        this.lastEvent = evt;

        if (this.handlers != null)
        {
            this.handlers.ForEach(handler =>
            {
                handler.Invoke(evt);
            });
        }
    }

    private void Unsubscribe(SimpleEventHandler handler)
    {
        Debug.Log("Unsubscribe");
        if (this.handlers == null)
        {
            return;
        }

        int idx = this.handlers.IndexOf(handler);
        if (idx != -1)
        {
            this.handlers.RemoveAt(idx);
        }

        if (this.handlers.Count == 0)
        {
            this.handlers = null;
        }
    }
}
