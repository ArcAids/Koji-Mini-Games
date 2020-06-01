using UnityEngine;

[SerializeField]
public class VCCMaterialColor : ManagedObject
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
        Material material = GetComponent<MeshRenderer>().sharedMaterials[0];

        if (color == null || material == null)
        {
            return;
        }
        if (color.a == 0)
            return;
        //Debug.Log("color updating: " + color.ToString()+ "on "+ material.name);
        //color.a = 1;
        material.SetColor("_BaseColor", color);
    }
}
