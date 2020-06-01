using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabler : MonoBehaviour
{
    [SerializeField] bool startOnAwake;
    [SerializeField] bool alsoDestroy;
    [SerializeField] float disableAfter;

    private void Awake()
    {
        if(startOnAwake)
            DisableWithDelay();
    }

    public void Destroy()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

    public void DisableWithDelay()
    {
        StartCoroutine(Disable());

    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(disableAfter);
        gameObject.SetActive(false);
        if(alsoDestroy)
            Destroy(gameObject);
    }
}
