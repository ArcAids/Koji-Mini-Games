using UnityEngine;

[SerializeField]
public class VCCParticlesColor : ManagedObject
{
    public string key;
          
    public void Start()
    {
        Handles.Add(
            KojiBridge.ObservableColorOfKey(key).DidChange.Subscribe(Observable_DidChange, true)
        );
        Observable_DidChange(Color.green);
    }

    private void Observable_DidChange(Color color)
    {
        ParticleSystem particles = GetComponent<ParticleSystem>();

        if (color == null || particles == null)
        {
            return;
        }
        if (color.a == 0)
            return;
        //color.a = 1;
        var main=particles.main;
        main.startColor = color;
    }
}
