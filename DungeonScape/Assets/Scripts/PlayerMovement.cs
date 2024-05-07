using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Vector2 moveInput;
    private Rigidbody2D playerRb;
    private Animator playerAnimator;
    private bool canInteract = false;
    private InteractableChest chest; // Referencia al script del cofre

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        playerAnimator.SetFloat("Vertical", moveY);
        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("Speed", moveInput.magnitude);

        // Si el jugador puede interactuar y presiona la tecla de interacción (por ejemplo, la tecla "E")
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            // Llamar al método de interacción del cofre
            chest.CheckCode("1234"); // Aquí debes pasar el código ingresado por el jugador
        }
    }

    void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);
    }

    // Método para activar la interacción cuando el jugador está cerca del cofre
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Chest"))
        {
            // Obtener una referencia al script del cofre
            chest = other.GetComponent<InteractableChest>();
            // Activar la interacción
            canInteract = true;
        }
    }

    // Método para desactivar la interacción cuando el jugador se aleja del cofre
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Chest"))
        {
            // Desactivar la interacción
            canInteract = false;
        }
    }
}
