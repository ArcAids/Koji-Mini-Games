using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    SceneController data;
    [Space]
    [Range(0, 1)]
    [SerializeField]
    float actualLoadingEndsAt = 0.5f;
    [SerializeField]
    float delayAfterActualLoading = 1f;
    [SerializeField]
    int iterationsForLoadingSimulation = 3;

    bool isLoading = false;
    bool isFading = false;
    bool loadLevelToo=true;
    bool showLoading=true;
    Animator loadingAnimator;
    int randomNumber;
    static LevelLoader Instance;

    public delegate void LevelLoadingEvents();
    public LevelLoadingEvents onFadeOutDone;
    public LevelLoadingEvents onLoadingDone;
    public LevelLoadingEvents onLoadingTick;
    public delegate void LevelLoadingProgressEvent(float progress);
    public LevelLoadingProgressEvent onProgress;

    readonly int fadeInHash = Animator.StringToHash("FadeIn");
    readonly int fadeOutHash = Animator.StringToHash("FadeOut");
    readonly int loadingHash = Animator.StringToHash("Loading");

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        loadingAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        data.Register(this);
    }
    private void OnDisable()
    {
        data.DeRegister(this);
    }

    public void PlayLevelTransition(bool showLoading=true)
    {
        if (isFading)
            return;
        this.showLoading = showLoading;
        loadLevelToo = true;
        UpdateLoadingProgress(0);
        FadeOut();
    }

    public void FadeOut()
    {
        isFading = true;
        loadingAnimator?.SetTrigger(fadeOutHash);
        //Debug.Log(loadingAnimator.GetAnimatorTransitionInfo(0).duration *1f);
    }

    public void OnFadeEnd()
    {
        if(loadLevelToo)
            data.StartLevelLoading();

        onFadeOutDone?.Invoke();

        if (showLoading)
        {
            StartCoroutine(Loading());
            StartCoroutine(OnLoadingTick());
        }
        else
            FadeIn();

    }

    IEnumerator Loading()
    {
        isLoading = true;
        loadingAnimator.SetBool(loadingHash, isLoading);


        while (data.Progress < 0.9f)
        {   
            UpdateLoadingProgress(data.Progress * actualLoadingEndsAt);
            yield return null;
        }

        UpdateLoadingProgress(actualLoadingEndsAt);

        randomNumber = (int)(actualLoadingEndsAt * 100);

        WaitForSeconds delayWait= new WaitForSeconds(delayAfterActualLoading / iterationsForLoadingSimulation);

        for (int i = 0; i < iterationsForLoadingSimulation; i++)
        {
            randomNumber = Random.Range(randomNumber, 100);
            UpdateLoadingProgress(randomNumber / 100f);
            yield return delayWait;
        }
        UpdateLoadingProgress(1);

        isLoading = false;
        loadingAnimator.SetBool(loadingHash, isLoading);

        onLoadingDone?.Invoke();
        FadeIn();
    }

    IEnumerator OnLoadingTick()
    {
        while (isLoading)
        {
            onLoadingTick?.Invoke();
            yield return null;
        }
    }

    private void FadeIn()
    {
        if(loadLevelToo)
            data.FinishLevelLoading();

        showLoading = false;
        loadLevelToo = false;
        isFading = false;
        loadingAnimator?.SetTrigger(fadeInHash);

    }
    void UpdateLoadingProgress(float progress)
    {
        onProgress.Invoke(progress);
    }

}
