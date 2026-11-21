
using System;
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TMP_Text dialogueText;
    public GameObject dialogueBox;

    public float letterDelay = 0.05f;

    public List<string> dialogueLines = new List<string>();

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

    public void StartConversation(List<string> newDialogue)
    {
        dialogueLines = new List<string>(newDialogue);
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

        currentDialogue = dialogueLines[currentLine];
        currentLine++;

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
