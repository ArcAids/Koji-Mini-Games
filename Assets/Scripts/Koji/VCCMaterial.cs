using UnityEngine;

[SerializeField]
public class VCCMaterial : ManagedObject
{
    public string key;
    public void Start()
    {
        Handles.Add(
            KojiBridge.ObservableMaterialOfKey(key).DidChange.Subscribe(Observable_DidChange, true)
        );

    }

    private void Observable_DidChange(Material material)
    {
        Material sharedMaterial = GetComponent<MeshRenderer>().sharedMaterials[0];

        if (material == null || sharedMaterial == null)
        {
            sharedMaterial.SetTexture("_BaseMap", null);
            return;
        }
        sharedMaterial.SetTexture("_BaseMap", material.mainTexture);
    }
}
