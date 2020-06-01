using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialoguePrinter : MonoBehaviour
{
    [SerializeField] DialogueSequence test;
    [SerializeField] UnityEvent onDialogueStart;
    List<Dialogue> conversation= new List<Dialogue>();
    public TMP_Text dialogueBox;
    public TMP_Text characterName;
    public bool autoNext;
    public float speed;
    int currentLine=-1;
    int dialogueNumber = -1;
    bool isDoneIterating = true;
    bool havingConversation=false;
    bool isInterruptable=true;

    public static DialoguePrinter Instance;
    Coroutine writer;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        gameObject.SetActive(false);
    }

    // Use this for initialization
    public void StartDialogue(DialogueSequence dialogues)
    {
        if(havingConversation && !isInterruptable)
        {
            if (dialogues.mustSpeak)
            {
                conversation.AddRange(dialogues.dialogues);
            }
            else return;
        }
        else
        {
            gameObject.SetActive(true);
            onDialogueStart.Invoke();
            Reset();
            conversation.AddRange(dialogues.dialogues);
            isInterruptable = dialogues.interruptable;
            autoNext = dialogues.autoScroll;
            havingConversation = true;
            Next();
        }
    }
    void Reset()
    {
        StopAllCoroutines();
        isDoneIterating = true;
        havingConversation = false;
        conversation.Clear();
        currentLine = -1;
        dialogueNumber = -1;
    }

    [ContextMenu("test")]
    public void TestDialogue()
    {
        StartDialogue(test);
    }

    IEnumerator IterateDialogue(string sentence)
    {
        isDoneIterating = false;
        foreach (var letter in sentence)
        {
            dialogueBox.text += letter;
            yield return new WaitForSeconds(speed);
        }
        isDoneIterating = true;
        if (autoNext)
        {
            yield return new WaitForSeconds(1);
            if(isDoneIterating && havingConversation)
                Next();
        }


    }

    public void Next()
    {
        if (!isDoneIterating)
            return;

        dialogueBox.text = "";
        currentLine++;
        //Debug.Log("clicked next " + conversation.Count);
        if (dialogueNumber==-1 || currentLine == conversation[dialogueNumber].lines.Count)
        {
            if (!LoadNextDialogue())
            {
                //dialogueBox.text = "Finish.";
                gameObject.SetActive(false);
                return;
            }
        }
        if(writer!=null)
            StopCoroutine(writer);
        writer=StartCoroutine(IterateDialogue(conversation[dialogueNumber].lines[currentLine]));
    }

    public void Back()
    {
        if (!isDoneIterating)
            return;

        isDoneIterating = false;

        if (currentLine > 0)
        {
            dialogueBox.text = "";
            currentLine--;
        }
    }

   bool LoadNextDialogue()
    {
        if (conversation.Count > dialogueNumber+1)
        {
            dialogueNumber++;
            characterName.text = conversation[dialogueNumber].characterName;
            currentLine = 0;
            Debug.Log("loaded dialogue" + conversation.Count);
            return true;
        }
        return false;
    }

    public void AutoSwitch()
    {
        autoNext = !autoNext;
    }
    private void OnDisable()
    {
        Reset();
    }
}
