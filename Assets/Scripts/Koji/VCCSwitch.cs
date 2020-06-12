using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class VCCSwitch : ManagedObject
{
    public string key;

    public void Start()
    {
        Handles.Add(
            KojiBridge.ObservableBooleanOfKey(key).DidChange.Subscribe(Observable_DidChange, true)
        );
    }

    private void Observable_DidChange(bool value)
    {
        gameObject.SetActive(value);
    }
}
