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
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private float textSpeed = 0.05f; // Velocidad del texto progresivo
    [SerializeField] private float lineDelay = 1.5f; // Tiempo entre líneas
    [SerializeField] private GameObject nextPopup; // Nuevo popup a mostrar

    private int currentLineIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDialoguePanel();
        }

        switch (currentState)
        {
            case DialogueState.DisplayingDialogue:
                // No se necesita hacer nada aquí, el diálogo se maneja en IEnumerator DisplayDialogue()
                break;
            case DialogueState.DisplayingNextPopup:
                // Activar el nuevo popup y cambiar al estado inactivo
                nextPopup.SetActive(true);
                currentState = DialogueState.Inactive;
                break;
            case DialogueState.Inactive:
                // En este estado, el diálogo está inactivo y no se está mostrando ningún popup
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
        DialoguePanel.SetActive(false); // Cierra el panel cuando el jugador se va
        StopAllCoroutines(); // Detiene cualquier corrutina que esté corriendo

        // Verificar si nextPopup no es nulo antes de desactivarlo
        if (nextPopup != null)
        {
            nextPopup.SetActive(false);
        }
    }
}


    private void ToggleDialoguePanel()
    {
        if (DialoguePanel.activeSelf)
        {
            DialoguePanel.SetActive(false);
            StopAllCoroutines();
            currentState = DialogueState.Inactive;
        }
        else
        {
            DialoguePanel.SetActive(true);
            currentLineIndex = 0; // Reinicia el índice de líneas cuando se abre el panel
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
                yield return new WaitForSeconds(textSpeed);
            }
            
            // Espera antes de mostrar la siguiente línea
            yield return new WaitForSeconds(lineDelay);
            currentLineIndex++;
        }

        // Cuando se acaben las líneas de diálogo, cambiar el estado para mostrar el próximo popup
        currentState = DialogueState.DisplayingNextPopup;
    }
}
