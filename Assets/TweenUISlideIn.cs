using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TweenUISlideIn : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Ease ease;
    RectTransform rect;
    Vector2 startingPosition;
    // Start is called before the first frame update
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        startingPosition = rect.position;
    }
    void OnEnable()
    {
        rect.position =new Vector2(startingPosition.x-rect.rect.width,startingPosition.y);
        rect.DOAnchorPosX(rect.position.x+rect.rect.width,speed);
    }

    [ContextMenu("Disable")]
    public void Disable()
    {
        rect.DOAnchorPosX(startingPosition.x - rect.rect.width, speed).onComplete = DisableNow;
    }
    public void DisableNow()
    {
        gameObject.SetActive(false);
    }
}
