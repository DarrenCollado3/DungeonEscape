using UnityEngine;

public class InteractableChest : MonoBehaviour
{
    public string requiredCode = "1234"; // Código requerido para interactuar
    public GameObject popupObject; // Referencia al GameObject con el SpriteRenderer de la ventana emergente

    private bool canActivate = false; // Variable para controlar si se puede activar la interacción
    private bool isInteracting = false; // Variable para controlar si el jugador está interactuando

    // Método para manejar la interacción del jugador
    void Update()
    {
        // Detectar la tecla de interacción (por ejemplo, la tecla "E")
        if (canActivate && Input.GetKeyDown(KeyCode.E))
        {
            isInteracting = true;
            // Aquí podrías mostrar un mensaje en la consola o realizar alguna otra acción
        }
    }

    // Método para verificar el código ingresado por el jugador
    public void CheckCode(string inputCode)
    {
        if (inputCode == requiredCode)
        {
            Debug.Log("¡Código correcto!");
            // Mostrar la ventana emergente
            ShowPopup();
            // Aquí puedes activar alguna acción en el juego, como abrir una puerta
            DisableInteraction(); // Desactivar la interacción después de ingresar el código correctamente
        }
        else
        {
            Debug.Log("¡Código incorrecto!");
            // Aquí puedes mostrar un mensaje al jugador indicando que el código es incorrecto
        }
        // Restablecer la interacción
        isInteracting = false;
    }

    // Método para mostrar la ventana emergente
    void ShowPopup()
    {
        popupObject.SetActive(true);
    }

    // Método para desactivar la interacción
    public void DisableInteraction()
    {
        canActivate = false;
    }

    // Método para activar la interacción cuando el jugador entra en el área de activación del cofre
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnableInteraction();
        }
    }

    // Método para desactivar la interacción cuando el jugador sale del área de activación del cofre
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisableInteraction();
        }
    }

    // Método para activar la interacción
    public void EnableInteraction()
    {
        canActivate = true;
    }
}
