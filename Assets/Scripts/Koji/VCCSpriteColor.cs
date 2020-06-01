using UnityEngine;

[SerializeField]
public class VCCSpriteColor : ManagedObject
{
    public string key;
    private SpriteRenderer spriteRenderer;
    private Observable<Color> observableColor;
    
    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        observableColor = KojiBridge.ObservableColorOfKey(key);
        Handles.Add(observableColor.DidChange.Subscribe(ObservableSpriteColor_DidChange, true));
    }


    private void ObservableSpriteColor_DidChange(Color color)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (color == null || spriteRenderer == null)
        {
            return;
        }
        if (color.a == 0)
            return;
        //color.a = 1;
        spriteRenderer.color = color;
    }
}
