using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class VCCUIColor : ManagedObject
{
    public string key;

    public void Start()
    {
        Handles.Add(
            KojiBridge.ObservableColorOfKey(key).DidChange.Subscribe(Observable_DidChange, true)
        );
    }

    private void Observable_DidChange(Color color)
    {
        Debug.Log("color updating: " + color.ToString());
        Image image = GetComponent<Image>();

        if (color == null || image == null)
        {
            return;
        }
        if (color.a == 0)
            return;
        image.color = color;
    }
}
