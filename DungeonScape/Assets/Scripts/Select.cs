using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject objeto; // GameObject a seleccionar
    [SerializeField] private GameObject canvasPopup; // Canvas que quieres mostrar como ventana emergente
    private bool isSelected;
    public float rayLength = 1f;
    public LayerMask RaycastLayer; // Capa a la que se aplicará el rayo
    
    // Start is called before the first frame update
    void Start()
    {
        // Desactiva el canvas al inicio
        canvasPopup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            Select();
    }

    private void Select()
    {
        // Lanza un rayo desde la posición del objeto hacia arriba
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, rayLength, RaycastLayer);
        
        // Dibujar el rayo en la escena
        Debug.DrawRay(transform.position, Vector2.up * rayLength, Color.red);

        // Verifica si el rayo ha golpeado un objeto en la capa "raycastdetect" y si es el objeto que estamos buscando
        if (hit.collider != null && hit.collider.gameObject == objeto)
        {
            // Cambia el color del objeto golpeado
            if (!isSelected)
            {
                isSelected = true;
                hit.collider.GetComponent<SpriteRenderer>().color = Color.red;
                
                // Activa el canvas como ventana emergente
                canvasPopup.SetActive(true);
                
                // Mensaje de depuración
                Debug.Log("Objeto seleccionado: " + objeto.name);
            }
            else
            {
                isSelected = false;
                hit.collider.GetComponent<SpriteRenderer>().color = Color.white; // Cambiar al color predeterminado cuando no está seleccionado
                
                // Desactiva el canvas
                canvasPopup.SetActive(false);
                
                // Mensaje de depuración
                Debug.Log("Objeto deseleccionado: " + objeto.name);
            }
        }
    }
}
