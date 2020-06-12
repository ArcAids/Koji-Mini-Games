using UnityEngine;
using DG.Tweening;

public class TweenHover : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float xDirection = 0;
    [SerializeField] float yDirection = 0;
    [SerializeField] Ease ease = Ease.InSine;
    [SerializeField] bool loops = true;
    [SerializeField] LoopType loopingType;

    // Start is called before the first frame update
    void Start()
    {
        Tween tween = null;
        Vector3 hoverTo;
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            hoverTo = new Vector2(rectTransform.anchoredPosition.x + xDirection, rectTransform.anchoredPosition.y + yDirection);
            tween = rectTransform.DOAnchorPos(hoverTo, speed).SetEase(ease).SetSpeedBased();
        }
        else
        {
            hoverTo = new Vector3(transform.localPosition.x + xDirection, transform.localPosition.y + yDirection, transform.localPosition.z);
            tween = transform.DOLocalMove(hoverTo, speed).SetEase(ease).SetSpeedBased();
        }


        if (loops)
            tween?.SetLoops(-1, loopingType);
        //iTween.MoveAdd(gameObject,iTween.Hash("y",yDirection, "x", xDirection,"EaseType", easeType,"LoopType",loopType,"speed",speed));
    }

}
