using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowinator : MonoBehaviour
{
    [SerializeField] float timeScale=0.5f;
    [SerializeField] float duration=1;
    [SerializeField] bool playOnEnable;
    [SerializeField] AnimationCurve transitionCurve;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if(playOnEnable)
            SlowTime();
    }

    public void SlowTime()
    {
        StopAllCoroutines();
        StartCoroutine(TimeSlow(timeScale-Time.timeScale,duration));
    }

    IEnumerator TimeSlow(float timeDifference, float duration)
    {
        //float transitionTime=0;
        //while (transitionTime<=1)
        //{
        //    transitionTime += Time.unscaledDeltaTime;
        //    Time.timeScale = 1+ (timeDifference * transitionCurve.Evaluate(transitionTime));
        //    Debug.Log(Time.timeScale);
        //    yield return null;
        //}
        Time.timeScale = Time.timeScale+timeDifference;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
    }
}
