using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Vector2 moveInput;
    private Rigidbody2D playerRb;
    private Animator playerAnimator;  // Added semicolon and corrected the variable name

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();  // Corrected the variable name
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;
        
        // Fixed the SetFloat calls
        playerAnimator.SetFloat("Vertical", moveY);
        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("speed", moveInput.magnitude);  // Changed sqrtMagnitude to magnitude
    }

    void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);
    }
}
