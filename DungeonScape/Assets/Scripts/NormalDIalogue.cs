using UnityEngine;
using TMPro;
using System.Collections;

public class NormalDialogue : MonoBehaviour
{
    public enum DialogueState
    {
        Inactive,
        DisplayingDialogue
    }

    private DialogueState currentState = DialogueState.Inactive;

    private bool isPlayerInRange;
    [SerializeField] private GameObject marquita;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private float textSpeed = 0.05f; // Speed of progressive text
    [SerializeField] private float lineDelay = 1.5f; // Time between lines
    [SerializeField] private AudioClip typingSound; // Audio clip para el sonido de tipeo
    private AudioSource audioSource;

    private int currentLineIndex = 0;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDialoguePanel();
        }

        switch (currentState)
        {
            case DialogueState.DisplayingDialogue:
                // Nothing needed here, dialogue is handled in IEnumerator DisplayDialogueCoroutine()
                break;
            case DialogueState.Inactive:
                // In this state, dialogue is inactive
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            marquita.SetActive(true);
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            marquita.SetActive(false);
            isPlayerInRange = false;

            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false); // Close the panel when the player leaves
            }

            StopAllCoroutines(); // Stop any running coroutines
        }
    }

    private void ToggleDialoguePanel()
    {
        if (dialoguePanel.activeSelf)
        {
            dialoguePanel.SetActive(false);
            StopAllCoroutines();
            currentState = DialogueState.Inactive;
        }
        else
        {
            dialoguePanel.SetActive(true);
            currentLineIndex = 0; // Reset the line index when the panel opens
            StartCoroutine(DisplayDialogueCoroutine());
            currentState = DialogueState.DisplayingDialogue;
        }
    }

    private IEnumerator DisplayDialogueCoroutine()
    {
        while (currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = "";
            foreach (char letter in dialogueLines[currentLineIndex].ToCharArray())
            {
                dialogueText.text += letter;
                PlayTypingSound();
                yield return new WaitForSeconds(textSpeed);
            }

            // Wait before showing the next line
            yield return new WaitForSeconds(lineDelay);
            currentLineIndex++;
        }

        // When all dialogue lines are finished, return to inactive state
        currentState = DialogueState.Inactive;
    }

    private void PlayTypingSound()
    {
        if (audioSource != null && typingSound != null)
        {
            audioSource.PlayOneShot(typingSound);
        }
    }
}
