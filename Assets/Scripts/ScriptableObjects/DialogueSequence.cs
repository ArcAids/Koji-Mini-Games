using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueSequence : ScriptableObject
{
    [SerializeField] public List<Dialogue> dialogues;
    [SerializeField] public bool autoScroll;
    [SerializeField] public bool pauseGame;
    [SerializeField] public bool mustSpeak=true;
    [SerializeField] public bool interruptable=false;

    
    public void Play()
    {
        hideFlags = HideFlags.DontSave;
        if(DialoguePrinter.Instance!=null)
            DialoguePrinter.Instance.StartDialogue(this);
    }
}

[System.Serializable]
public class Dialogue
{
    public string characterName;
    public List<string> lines;
}