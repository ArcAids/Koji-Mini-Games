using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class VCCUISprite : ManagedObject
{
    public string key;

    public void Start()
    {
        image = GetComponent<Image>();

        observableSprite = KojiBridge.ObservableSpriteOfKey(key);
        Handles.Add(observableSprite.DidChange.Subscribe(ObservableSprite_DidChange, true));
    }

    private Image image;
    private Observable<Sprite> observableSprite;

    private void ObservableSprite_DidChange(Sprite sprite)
    {
        if(image==null)
        image = GetComponent<Image>();

        if (sprite == null || image == null)
        {
            return;
        }

        image.sprite = sprite;

    }
}
