using UnityEngine;
using DG.Tweening;
public class TweenRectShift : MonoBehaviour
{
    [SerializeField] bool autoEnable=false;
    [SerializeField] float duration = 1;
    [SerializeField] Ease ease;
    [SerializeField] RectTransform targetRect;
    [SerializeField] bool controlTargetRect=false;


    RectTransform rectTransform;
    Vector2 originalPosition;
    Vector2 sizeDelta;
    Vector2 pivot;
    Vector2 anchorMax;
    Vector2 anchorMin;
    private void Awake()
    {
        TryGetComponent(out rectTransform);

        if (controlTargetRect)
        {
            RectTransform rectTransform;
            rectTransform = targetRect;
            targetRect = this.rectTransform;
            this.rectTransform = rectTransform;
        }

        originalPosition = rectTransform.position;
        pivot = rectTransform.pivot;
        sizeDelta = rectTransform.sizeDelta;
        anchorMin = rectTransform.anchorMin;
        anchorMax = rectTransform.anchorMax;

    }
    void OnEnable()
    {
        if (autoEnable)
        {
            TweenEnable(duration);
        }
    }

    public void TweenEnable(float duration)
    {
        Reset();
        rectTransform.ShiftTo(targetRect, duration, ease);
    }
    
    private void Reset()
    {
        rectTransform.pivot = pivot;
        rectTransform.sizeDelta = sizeDelta;
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
        rectTransform.position = originalPosition;
    }

    [ContextMenu("TweenDisable")]
    public void TweenDisable()
    {
        TweenDisable(duration);
    }
    public void TweenDisable(float duration)
    {
        rectTransform.ShiftTo(anchorMin, anchorMax, pivot, sizeDelta,originalPosition,duration, ease);
    }

    private void OnDisable()
    {
        if(controlTargetRect && autoEnable)
            TweenDisable(duration);
    }
    public void DisableNow()
    {
        gameObject.SetActive(false);
    }
}

public static class ExtensionClass
{
    public static void ShiftTo(this RectTransform rectTransform, RectTransform target, float duration, Ease ease)
    {
        rectTransform.ShiftTo(target.anchorMin, target.anchorMax, target.pivot, target.sizeDelta,target.position, duration,ease);
    }

    public static void ShiftTo(this RectTransform rectTransform, Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot, Vector2 sizeDelta,Vector2 position, float duration, Ease ease)
    {
        rectTransform.DOAnchorMax(anchorMax, duration).SetEase(ease);
        rectTransform.DOAnchorMin(anchorMin, duration).SetEase(ease);
        rectTransform.DOPivot(pivot, duration).SetEase(ease);
        rectTransform.DOSizeDelta(sizeDelta, duration).SetEase(ease);
        rectTransform.DOMove(position,duration).SetEase(ease);
    }
}