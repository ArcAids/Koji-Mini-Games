using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TweenRotate : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float xDirection;
    [SerializeField] float yDirection;
    [SerializeField] Ease ease = Ease.InSine;
    [SerializeField] bool loops = true;
    [SerializeField] LoopType loopingType;


    // Start is called before the first frame update
    void Start()
    {
        //iTween.RotateAdd(gameObject,iTween.Hash("y",yDirection, "x", xDirection,"EaseType", easeType,"LoopType",loopType,"speed",speed,"fixe"));
        Tween tween=transform.DOLocalRotate(new Vector3(transform.localRotation.eulerAngles.x+xDirection,transform.localRotation.eulerAngles.y +yDirection),speed)
            .SetSpeedBased().SetEase(ease);
        if (loops)
            tween.SetLoops(-1, loopingType);
    }

}
