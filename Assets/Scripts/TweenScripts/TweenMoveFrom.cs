using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TweenMoveFrom : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float xDirection;
    [SerializeField] float yDirection;
    [SerializeField] Ease ease = Ease.InSine;
    [SerializeField] bool loops=true;
    [SerializeField] LoopType loopingType;

    Vector3 originalPosition;

    // Start is called before the first frame update
    void Awake()
    {
        originalPosition=transform.localPosition;
        //Play();
    }
    public void Play()
    {
        //iTween.MoveFrom(gameObject,iTween.Hash("y",yDirection, "x", xDirection,"EaseType", easeType,"LoopType",loopType,"speed",speed));
        transform.localPosition = new Vector3(xDirection,yDirection,originalPosition.z);
        Tween tween= transform.DOLocalMove(originalPosition, speed).SetSpeedBased().SetEase(ease);
        if (loops)
            tween.SetLoops(-1, loopingType);
    }

}
