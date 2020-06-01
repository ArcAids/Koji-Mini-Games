using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LoadingBar : MonoBehaviour
{
    LevelLoader loader;
    Image loadingBar;
    [SerializeField]
    Gradient colorShift;
    private void Awake()
    {
        loader = GetComponentInParent<LevelLoader>();
        loadingBar = GetComponent<Image>();
    }

    private void OnEnable()
    {
        if (loader != null)
        {
            loader.onProgress += SetProgress;
        }
    }

    private void OnDisable()
    {
        if (loader != null)
        {
            loader.onProgress -= SetProgress;
        }
    }

    void SetProgress(float progress) 
    {
        loadingBar.fillAmount = progress;
        loadingBar.color = colorShift.Evaluate(progress);
    }

}
