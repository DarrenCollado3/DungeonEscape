using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOp : MonoBehaviour
{
    [SerializeField] private Animator animatorController;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            animatorController.SetBool("Open", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            animatorController.SetBool("Open", false);
        }
    }
}
