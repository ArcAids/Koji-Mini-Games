using UnityEngine;

[SerializeField]
public class VCCColor : ManagedObject
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
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        if (color == null || meshRenderer == null || meshRenderer.material == null)
        {
            return;
        }

        meshRenderer.material.color = color;
    }
}
