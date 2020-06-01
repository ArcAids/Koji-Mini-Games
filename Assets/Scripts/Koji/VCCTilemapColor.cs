using UnityEngine;
using UnityEngine.Tilemaps;

[SerializeField]
public class VCCTilemapColor : ManagedObject
{
    public string key;
    private Tilemap tilemap;
    private Observable<Color> observableColor;

    public void Start()
    {
        tilemap = GetComponent<Tilemap>();

        observableColor = KojiBridge.ObservableColorOfKey(key);
        Handles.Add(observableColor.DidChange.Subscribe(ObservableSpriteColor_DidChange, true));
    }


    private void ObservableSpriteColor_DidChange(Color color)
    {
        tilemap= GetComponent<Tilemap>();

        if (color == null || tilemap == null)
        {
            return;
        }
        if (color.a == 0)
            return;
        //color.a = 1;
        tilemap.color = color;
    }
}
