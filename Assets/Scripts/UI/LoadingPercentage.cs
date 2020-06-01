using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class LoadingPercentage : MonoBehaviour
{
    [SerializeField] string format="{0}%";
    TMP_Text textBox;
    LevelLoader loader;
    private void Awake()
    {
        textBox = GetComponent<TMP_Text>();
        loader = GetComponentInParent<LevelLoader>();

    }

    private void OnEnable()
    {
        if (loader != null)
        {
            loader.onProgress += SetPercentage;
        }
    }

    private void OnDisable()
    {
        if (loader != null)
        {
            loader.onProgress -= SetPercentage;
        }
    }
    public void SetPercentage(float percentage)
    {
        percentage = (float)decimal.Round((decimal)percentage, 2);
        textBox.text = string.Format(format, percentage*100);
    }
}
