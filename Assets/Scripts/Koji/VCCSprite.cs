using UnityEngine;

public enum ScaleMode
{
    Fit,
    Fill,
    Stretch,
    None
};

[SerializeField]
public class VCCSprite : ManagedObject
{
    public string key;
    public ScaleMode scaleMode;
    
    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Vector3 originalBounds = spriteRenderer.sprite.bounds.size;
        Vector3 originalScale = spriteRenderer.transform.localScale;

        originalSize = new Vector3(
            originalBounds.x * originalScale.x,
            originalBounds.y * originalScale.y,
            originalBounds.z * originalScale.z
        );

        originalTileSize = spriteRenderer.size;

        observableSprite = KojiBridge.ObservableSpriteOfKey(key);
        Handles.Add(observableSprite.DidChange.Subscribe(ObservableSprite_DidChange, true));
    }

    private SpriteRenderer spriteRenderer;
    private Observable<Sprite> observableSprite;
    private Vector3 originalSize;
    private Vector2 originalTileSize;

    private void ObservableSprite_DidChange(Sprite sprite)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (sprite == null || spriteRenderer == null)
        {
            return;
        }

        spriteRenderer.sprite = sprite;
        spriteRenderer.color = Color.white;
        Vector3 newSize = sprite.bounds.size;

        if (scaleMode == ScaleMode.Stretch)
        {
            float scaleX = originalSize.x / newSize.x;
            float scaleY = originalSize.y / newSize.y;

            spriteRenderer.transform.localScale = new Vector3(scaleX, scaleY, 1f);

            if (spriteRenderer.drawMode == SpriteDrawMode.Tiled)
            {
                spriteRenderer.size = new Vector2(spriteRenderer.size.x, newSize.y / 100f);
            }
            else
            {
                spriteRenderer.size = originalTileSize;
            }
        }
        else if (scaleMode == ScaleMode.Fit)
        {
            
            float scaleX = originalSize.x / newSize.x;
            float scaleY = originalSize.y / newSize.y;
            float scale = Mathf.Min(scaleX, scaleY);

            spriteRenderer.transform.localScale = new Vector3(scale, scale, 1f);

            if (spriteRenderer.drawMode == SpriteDrawMode.Tiled)
            {
                float previousAspectRatio = spriteRenderer.size.x / spriteRenderer.size.y;
                float newWidth = newSize.y * previousAspectRatio;

                spriteRenderer.size = new Vector2(newWidth, newSize.y);
            }
            else
            {
                spriteRenderer.size = originalTileSize;
            }
        }
        else if (scaleMode == ScaleMode.Fill)
        {
            float scaleX = originalSize.x / newSize.x;
            float scaleY = originalSize.y / newSize.y;
            float scale = Mathf.Max(scaleX, scaleY);

            spriteRenderer.transform.localScale = new Vector3(scale, scale, 1f);

            if (spriteRenderer.drawMode == SpriteDrawMode.Tiled)
            {
                spriteRenderer.size = originalTileSize / new Vector2(scale, scale);
            }
            else
            {
                spriteRenderer.size = originalTileSize;
            }
        }
    }
}
