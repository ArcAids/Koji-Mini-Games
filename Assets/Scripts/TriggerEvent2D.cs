using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TriggerEvent2D : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggerEnter;

    [SerializeField] List<string> tags;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var tag in tags)
        {
            if(collision.CompareTag(tag))
                onTriggerEnter.Invoke();
        }
    }
}
