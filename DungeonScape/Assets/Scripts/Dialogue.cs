using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    public enum DialogueState
    {
        Inactive,
        DisplayingDialogue,
        DisplayingNextPopup
    }

    private DialogueState currentState = DialogueState.Inactive;

    private bool isPlayerInRange;
    [SerializeField] private GameObject marquita;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private float textSpeed = 0.05f; // Speed of progressive text
    [SerializeField] private float lineDelay = 1.5f; // Time between lines
    [SerializeField] private GameObject nextPopup; // Next popup to display
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
        audioSource.loop = false; // Ensure the audio is not looping
        audioSource.playOnAwake = false; // Ensure the audio does not play on awake
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
                // Nothing needed here, dialogue is handled in IEnumerator DisplayDialogue()
                break;
            case DialogueState.DisplayingNextPopup:
                // Activate the new popup and change to the inactive state
                if (nextPopup != null)
                {
                    nextPopup.SetActive(true);
                    // Mark the item as obtained
                    PlayerInventory.instance.ObtainItem("SpecialItem");
                }
                currentState = DialogueState.Inactive;
                break;
            case DialogueState.Inactive:
                // In this state, dialogue is inactive and no popup is displayed
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

            // Verifica si dialoguePanel es nulo antes de intentar desactivarlo
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false); // Close the panel when the player leaves
            }

            StopAllCoroutines(); // Stop any running coroutines

            // Deactivate nextPopup if it is not null
            if (nextPopup != null)
            {
                nextPopup.SetActive(false);
            }
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
            StartCoroutine(DisplayDialogue());
            currentState = DialogueState.DisplayingDialogue;
        }
    }

    private IEnumerator DisplayDialogue()
    {
        while (currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = "";
            foreach (char letter in dialogueLines[currentLineIndex].ToCharArray())
            {
                dialogueText.text += letter;
                PlayTypingSound2();
                yield return new WaitForSeconds(textSpeed);
            }

            // Wait before showing the next line
            yield return new WaitForSeconds(lineDelay);
            currentLineIndex++;
        }

        // When all dialogue lines are finished, change the state to display the next popup
        currentState = DialogueState.DisplayingNextPopup;
    }

    private void PlayTypingSound2()
    {
        if (audioSource != null && typingSound != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(typingSound);
        }
    }

    private void PlayTypingSound()
    {
        if (audioSource != null && typingSound != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(typingSound);
        }
    }
}
