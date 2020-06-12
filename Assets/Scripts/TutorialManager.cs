using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] bool startOnAwake = true;
    [SerializeField] UnityEvent onFinishTutorial;
    [SerializeField] GameObject tutorialWindow;

    const string showTutorialPrefString= "Tutorial";

    public bool TutorialToShow { get => PlayerPrefs.GetInt(showTutorialPrefString, 1) == 1; set => PlayerPrefs.SetInt(showTutorialPrefString, value ? 1 : 0); }

    private void Start()
    {
        if (startOnAwake)
            StartTutorial();
    }

    public void EnableTutorialAgain()
    {
        TutorialToShow = true;
    }

    public void StartTutorial()
    {
        if (TutorialToShow)
        {
            tutorialWindow.SetActive(true);
        }
        else
        {
            FinishTutorial();
        }
    }

    public void FinishTutorial()
    {
        tutorialWindow.SetActive(false);
        onFinishTutorial?.Invoke();
    }

    public void OnNeverShowTutorialToggleUpdate(bool value)
    {
        TutorialToShow = !value;
    }
}
