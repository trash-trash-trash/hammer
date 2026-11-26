
using System;
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TMP_Text dialogueText;
    public TMP_Text speakerName01Text;
    public TMP_Text speakerName02Text;
    public GameObject dialogueBox;

    public float letterDelay = 0.05f;

    public List<DialogueLine> dialogueLines = new List<DialogueLine>();

    public GameObject currentSpeaker;

    private int currentLine;
    private Coroutine typingCoroutine;
    private bool isTyping;
    private string currentDialogue;

    public event Action AnnounceDialogueDone;

    private void Awake()
    {
        Instance = this;
        dialogueBox.SetActive(false);
    }

    public void StartConversation(List<DialogueLine> newDialogue)
    {
        dialogueLines = new List<DialogueLine>(newDialogue);
        currentLine = 0;

        dialogueBox.SetActive(true);
        dialogueText.text = "";

        AdvanceDialogue();
    }

    public void AdvanceDialogue()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentDialogue;
            isTyping = false;
            typingCoroutine = null;
            return;
        }

        LoadNextDialogue();
    }

    private void LoadNextDialogue()
    {
        if (currentLine >= dialogueLines.Count)
        {
            EndConversation();
            return;
        }

        DialogueLine line = dialogueLines[currentLine];
        currentLine++;
        

        currentDialogue = line.dialogue;

        if(line.isPlayer)
        {
            speakerName01Text.text = line.speakerName;
            speakerName02Text.text = "";
        }
        else
        {
            speakerName01Text.text = "";
            speakerName02Text.text = line.speakerName;
        }
        
        
        currentSpeaker = line.speaker;

        typingCoroutine = StartCoroutine(TypeDialogue());
    }

    private IEnumerator TypeDialogue()
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in currentDialogue)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }

        isTyping = false;
        typingCoroutine = null;
    }

    public void EndConversation()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        isTyping = false;
        currentLine = 0;
        currentDialogue = "";

        dialogueLines.Clear();
        dialogueText.text = "";

        dialogueBox.SetActive(false);
        
        AnnounceDialogueDone?.Invoke();
    }
}
