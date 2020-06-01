using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VCCTMPStrings : ManagedObject
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
        TMP_Text text = GetComponent<TMP_Text>();

        if (value == null || text == null)
        {
            return;
        }

        text.text = value;
    }
}
