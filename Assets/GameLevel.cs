using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    SpriteRenderer[] sprites;
    private void Awake()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }
    public void SetIndex(int index)
    {
        
        float value = (5f - index) / 5f;
        foreach (var sprite in sprites)
        {
            sprite.sortingOrder = 6-index;
        }
        StartCoroutine(SwitchColor(new Color(value, value, value)));
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
}
