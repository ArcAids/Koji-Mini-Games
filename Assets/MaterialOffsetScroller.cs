using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOffsetScroller : MonoBehaviour
{
    Material material;
    float speed=0.2f;
    Vector2 direction;
    public bool canPlay = false;
    bool goingRight = true;
    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void ChangeDirection()
    {
        if (goingRight)
        {
            goingRight = false;

            direction.x=speed;
            direction.y = 0;
        }
        else
        {
            goingRight = true;
            direction.x = 0;
            direction.y=-speed;
        }
    }
    private void Update()
    {
        material.SetTextureOffset("_BaseMap",Time.time*direction);
        if (!canPlay) return;
    }
}
