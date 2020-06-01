using UnityEngine;
using UnityEngine.UI;
using TMPro;

[SerializeField]
public class VCCString : ManagedObject
{
    public string key;

    public void Start()
    {
        Handles.Add(
            KojiBridge.ObservableStringOfKey(key).DidChange.Subscribe(Observable_DidChange, true)
        );
    }

    private void Observable_DidChange(string value)
    {
        Text text = GetComponent<Text>();

        if (value == null || text == null)
        {
            return;
        }

        text.text = value;
    }
}
