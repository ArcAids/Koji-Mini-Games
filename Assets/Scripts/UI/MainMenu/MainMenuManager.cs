using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using levelManagement;
namespace GameSettingsUI
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField]
        UnityEngine.Events.UnityEvent onStart;
        [SerializeField]
        GameSettingsMenu gameSettings;
        [SerializeField]
        SceneController loader;
        [SerializeField]
        bool showPromptBeforeExit=true;
        [SerializeField]
        GameObject exitPrompt;

        private void Start()
        {
            onStart?.Invoke();
            gameSettings.LoadSettings();
        }

        public void LoadGame()
        {
            loader.Load(1);
        }

        public void AttemptExitGame()
        {
            if (showPromptBeforeExit)
                exitPrompt.SetActive(true);
            else
                QuitGame();
        }

        public void QuitGame()
        {
                Application.Quit();
        }
    }
}
