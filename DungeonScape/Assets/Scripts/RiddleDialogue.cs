using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueWithRiddle : MonoBehaviour
{
    public enum DialogueState
    {
        Inactive,
        DisplayingDialogue,
        DisplayingRiddle,
        DisplayingResult
    }

    public DialogueState currentState = DialogueState.Inactive;

    private bool isPlayerInRange;
    [SerializeField] private GameObject marquita;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private float textSpeed = 0.05f; // Speed of progressive text
    [SerializeField] private float lineDelay = 1.5f; // Time between lines
    [SerializeField] private GameObject riddlePopup;
    [SerializeField] private TMP_Text riddleQuestionText;
    [SerializeField] private TMP_InputField playerInput;
    [SerializeField] private GameObject rewardPopup;
    [SerializeField] private string correctAnswer;
    [SerializeField] private string correctAnswerResponse;
    [SerializeField] private string incorrectAnswerResponse;

    private int currentLineIndex = 0;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            switch (currentState)
            {
                case DialogueState.Inactive:
                    StartDialogue();
                    break;
                case DialogueState.DisplayingDialogue:
                    // No hacer nada mientras se muestra el diálogo
                    break;
                case DialogueState.DisplayingRiddle:
                    // No hacer nada mientras se muestra el acertijo
                    break;
                case DialogueState.DisplayingResult:
                    // No hacer nada mientras se muestra el resultado
                    break;
            }
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
            // Desactivar todos los popups
            marquita.SetActive(false);
            isPlayerInRange = false;
            dialoguePanel.SetActive(false);
            riddlePopup.SetActive(false);
            rewardPopup.SetActive(false);
            currentState = DialogueState.Inactive;
            StopAllCoroutines();
        }
    }


    private void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        StartCoroutine(DisplayDialogue());
        currentState = DialogueState.DisplayingDialogue;
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
            // Wait before showing the next line
            yield return new WaitForSeconds(lineDelay);
            currentLineIndex++;
        }

        // When all dialogue lines are finished, change the state to display the riddle
        DisplayRiddle();
    }

    private void DisplayRiddle()
    {
        currentState = DialogueState.DisplayingRiddle;
        dialoguePanel.SetActive(false);
        riddlePopup.SetActive(true);
        riddleQuestionText.text = "¿Cuántas calaveras hay en la habitación?";
        playerInput.gameObject.SetActive(true);
        playerInput.text = ""; // Limpiar el texto del InputField
        playerInput.ActivateInputField(); // Activar el InputField para que el jugador pueda ingresar su respuesta
    }

    public void CheckAnswer()
    {
        string playerAnswer = playerInput.text.Trim().ToLower();
        if (playerAnswer == correctAnswer.ToLower())
        {
            // Respuesta correcta
            Debug.Log("Respuesta correcta");
            riddlePopup.SetActive(false); // Ocultar el popup del acertijo
                                          // Aquí puedes mostrar un mensaje adicional o realizar alguna acción
                                          // Muestra el popup de recompensa
            rewardPopup.SetActive(true);
            PlayerInventory.instance.ObtainItem("SpecialItem");
            currentState = DialogueState.Inactive; // Cambia el estado a Inactivo
        }
        else
        {
            // Respuesta incorrecta
            Debug.Log("Respuesta incorrecta");
            riddleQuestionText.text = incorrectAnswerResponse;
            playerInput.text = ""; // Limpiar el texto del InputField para que el jugador pueda intentar de nuevo
                                   // Aquí puedes mostrar un mensaje adicional o realizar alguna acción
        }
    }


    public void SubmitAnswer()
    {
        // Método que maneja el envío del InputField
        CheckAnswer();
    }
}
