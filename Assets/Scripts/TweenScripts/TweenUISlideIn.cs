using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TweenUISlideIn : MonoBehaviour
{
    [SerializeField] bool autoEnable=true;
    [SerializeField] float duration;
    [SerializeField] float delay;
    [SerializeField] Ease ease;
    [SerializeField] Vector2 direction;
    RectTransform rect;
    Vector2 enabledPosition;
    Vector2 disabledPosition;
    // Start is called before the first frame update
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        enabledPosition = rect.anchoredPosition;
        disabledPosition = new Vector2(enabledPosition.x - (enabledPosition.x + rect.rect.width) * direction.x, enabledPosition.y-(-enabledPosition.y + rect.rect.height) * direction.y);
    }

    //Vector2 GetMargins(Vector2 direction)
    //{
    //    Vector2 margins;
    //    if(direction.x>1)
    //    {
    //        margins.x=rect.localPosition.x;
    //    }else
    //    {

    //    }
    //    return margins;
    //}

    public void TweenEnable()
    {
        TweenEnable(duration);
    }
    public void TweenEnable(float duration)
    {
        rect.DOAnchorPos(enabledPosition, duration).SetEase(ease);
    }

    private void Reset()
    {
        rect.anchoredPosition =disabledPosition;
    }
    void OnEnable()
    {
        Reset();
        if (autoEnable)
            Invoke("TweenEnable", delay);
    }

    [ContextMenu("TweenDisable")]
    public void TweenDisable()
    {
        TweenDisable(duration);
    }
    public void TweenDisable(float duration)
    {
        rect.DOAnchorPos(disabledPosition, duration).SetEase(ease).onComplete = DisableNow;
    }
    public void DisableNow()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Reset();
    }
}
