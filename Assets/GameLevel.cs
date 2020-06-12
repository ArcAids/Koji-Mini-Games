using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    SpriteRenderer[] sprites;
    static Color color;
    private void Awake()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        //color = sprites[0].color;
        color = Color.black;
    }
    public void SetIndex(int index)
    {
        //color.a = 0;
        float value = index /8f;
        foreach (var sprite in sprites)
        {
            if (value == 1)
                sprite.color=color;
            sprite.sortingOrder =9-index;
        }
            
        StartCoroutine(SwitchColor(Color.Lerp(Color.white,Color.black,value)));
        //StartCoroutine(SwitchOpacity( value));
    }

    IEnumerator SwitchColor(Color color)
    {
        float timer = 1;
        while (timer>0)
        {
            foreach (var sprite in sprites)
            {
                sprite.color = Color.Lerp(sprite.color,color,1-timer);
            }
            timer -= Time.deltaTime * 0.4f;
            yield return null;
        }
    }
    IEnumerator SwitchOpacity(float value)
    {
        float timer = 1;
        while (timer>0)
        {
            foreach (var sprite in sprites)
            {
                color.a= Mathf.Lerp(sprite.color.a,value,1-timer);
                sprite.color = color;
            }
            timer -= Time.deltaTime * 0.4f;
            yield return null;
        }
    }
}
