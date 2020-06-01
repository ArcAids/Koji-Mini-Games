using UnityEngine;

[SerializeField]
public class VCCMainCameraColor : ManagedObject
{
    public string key;
    private Camera camera;
    private Observable<Color> observableColor;

    public void Start()
    {
        camera = GetComponent<Camera>();

        observableColor = KojiBridge.ObservableColorOfKey(key);
        Handles.Add(observableColor.DidChange.Subscribe(ObservableSpriteColor_DidChange, true));
    }


    private void ObservableSpriteColor_DidChange(Color color)
    {
        camera = GetComponent<Camera>();

        if (color == null || camera == null)
        {
            return;
        }

        color.a = 1;
        camera.backgroundColor = color;
    }
}
