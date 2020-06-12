
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class TweenScale : MonoBehaviour
{
    [SerializeField] bool startOnAwake = true;
    [SerializeField] float delay=0;
    [SerializeField] float speed;
    [SerializeField] public float newXScale;
    [SerializeField] public float newYScale;
    [SerializeField] Ease ease = Ease.InSine;
    [SerializeField] bool loops = true;
    [SerializeField] LoopType loopingType;

    Vector3 originalScale;
    Vector3 scaleTo;

    // Start is called before the first frame update
    void Awake()
    {
        originalScale = transform.localScale;
        scaleTo = new Vector3(newXScale, newYScale, transform.localScale.z);
    }
    private void OnEnable()
    {
        transform.localScale = originalScale;
        if (startOnAwake)
            Invoke("StartTween",delay);
    }

    public void StartTween()
    {
        Tween tween = transform.DOScale(scaleTo, speed).SetEase(ease);
        if (loops)
            tween.SetLoops(-1, loopingType);
        //iTween.ScaleTo(gameObject, iTween.Hash("y", transform.localScale.y * yPercentage, "x", transform.localScale.x * xPercentage, "EaseType", easeType, "LoopType", loopType, "speed", speed));
    }
}
