using UnityEngine;

[SerializeField]
public class VCCLightColor : ManagedObject
{
    public string key;
    private new Light light;
    private Observable<Color> observableColor;

    public void Start()
    {
        light = GetComponent<Light>();

        observableColor = KojiBridge.ObservableColorOfKey(key);
        Handles.Add(observableColor.DidChange.Subscribe(ObservableSpriteColor_DidChange, true));
    }


    private void ObservableSpriteColor_DidChange(Color color)
    {
        light = GetComponent<Light>();

        if (color == null || light == null)
        {
            return;
        }
        color.a = 1;
        light.color = color;
    }
}
