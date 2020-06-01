using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PivotSpinner : MonoBehaviour
{
    enum loopType{
        NONE, LOOP, YOYO
    }
    [SerializeField] Transform pivot;
    [SerializeField] float speed=1;
    [SerializeField] float startAngle;
    [SerializeField] float arcLength;
    [SerializeField] loopType loop;
    
    float targetRotation;
    Vector3 rotation;
    bool isSpinning = true;
    float currentRotation;

    public float Speed { get => speed; set => speed = value; }

    public void StartSpinning()
    {
        StopAllCoroutines();
        StartCoroutine(KeepSpinning());
    }
    IEnumerator KeepSpinning()
    {
        currentRotation=0;
        float startRotationNormalized = startAngle / 359;
        float targetRotationNormalized = (startAngle + arcLength) / 359;
        isSpinning = true;
        bool reachedDestination=false;
        while (isSpinning)
        {
            if(loop==loopType.YOYO && reachedDestination)
            {
                float temp = startRotationNormalized;
                startRotationNormalized=targetRotationNormalized;
                targetRotationNormalized=temp;
                reachedDestination = false;
                currentRotation = startRotationNormalized;
                Speed *= -1;
            }
            currentRotation += Time.deltaTime * Speed *0.1f;
            currentRotation=Mathf.Clamp(currentRotation, Mathf.Min(startRotationNormalized, targetRotationNormalized), Mathf.Max(startRotationNormalized, targetRotationNormalized));
            //Debug.Log((speed >= 0 ? currentRotation >= targetRotationNormalized : currentRotation <= targetRotationNormalized) + ":"+ currentRotation+":"+targetRotationNormalized);
            if (currentRotation==targetRotationNormalized)
            {
                reachedDestination = true;
            }
            SetPivotRotation(currentRotation);

            yield return null;
        }
    }

    public void SetTargetRotation(float ratio)
    {
        targetRotation = ratio * 359;
    }
    IEnumerator Spin()
    {
        while(pivot.localRotation.eulerAngles.z!= targetRotation)
        {
            SetPivotRotationInEulerAngle(Mathf.MoveTowardsAngle(pivot.localRotation.eulerAngles.z,targetRotation,Time.deltaTime*Speed));
            yield return null;
        }
    }

    public float StopSpinning()
    {
        isSpinning = false;
        SetPivotRotation(currentRotation);
        return currentRotation;
    }
    void SetPivotRotation(float ratio)
    {
        SetPivotRotationInEulerAngle(ratio * 359);
    }
    void SetPivotRotationInEulerAngle(float rotationAmount)
    {
        rotation.z =rotationAmount;
        pivot.localRotation = Quaternion.Euler(rotation);
    }

    public void Reset()
    {
        SetPivotRotation(startAngle);
    }

    public void Hide()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
}
