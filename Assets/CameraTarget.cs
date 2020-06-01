using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] float levelSwitchSpeed = 5;

    float currentHeight=0;
    private void Awake()
    {
        currentHeight = transform.position.y;
    }
    public void MoveHigher(float height)
    {
        StopAllCoroutines();
        transform.position = Vector2.up * currentHeight;
        StartCoroutine(MoveCameraTarget(transform.position.y + height));
    }

    IEnumerator MoveCameraTarget(float yPosition)
    {
        currentHeight = yPosition;
        while (transform.position.y != yPosition)
        {
            transform.position = Vector2.up * Mathf.MoveTowards(transform.position.y, yPosition, levelSwitchSpeed * Time.deltaTime);
            yield return null;
        }
    }

}
