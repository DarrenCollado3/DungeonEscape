using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> objetos; // Lista de GameObjects a seleccionar
    [SerializeField] private List<GameObject> canvasPopups; // Lista de Canvas que quieres mostrar como ventanas emergentes
    private bool[] isSelected; // Array de booleanos para rastrear el estado de selección de cada objeto
    public float rayLength = 1f;
    public LayerMask RaycastLayer; // Capa a la que se aplicará el rayo
    
    // Start is called before the first frame update
    void Start()
    {
        // Inicializa el array de isSelected con el tamaño de la lista de objetos
        isSelected = new bool[objetos.Count];

        // Desactiva todos los canvasPopups al inicio
        foreach (GameObject canvasPopup in canvasPopups)
        {
            canvasPopup.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Select();
        }
    }

    private void Select()
    {
        // Lanza un rayo desde la posición del objeto hacia arriba
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, rayLength, RaycastLayer);
        
        // Dibujar el rayo en la escena
        Debug.DrawRay(transform.position, Vector2.up * rayLength, Color.red);

        // Verifica si el rayo ha golpeado un objeto en la capa "raycastdetect" y si es uno de los objetos que estamos buscando
        for (int i = 0; i < objetos.Count; i++)
        {
            if (hit.collider != null && hit.collider.gameObject == objetos[i])
            {
                // Cambia el color del objeto golpeado
                if (!isSelected[i])
                {
                    isSelected[i] = true;
                    hit.collider.GetComponent<SpriteRenderer>().color = Color.red;
                    
                    // Activa el canvas correspondiente como ventana emergente
                    canvasPopups[i].SetActive(true);
                    
                    // Mensaje de depuración
                    Debug.Log("Objeto seleccionado: " + objetos[i].name);
                }
                else
                {
                    isSelected[i] = false;
                    hit.collider.GetComponent<SpriteRenderer>().color = Color.white; // Cambiar al color predeterminado cuando no está seleccionado
                    
                    // Desactiva el canvas correspondiente
                    canvasPopups[i].SetActive(false);
                    
                    // Mensaje de depuración
                    Debug.Log("Objeto deseleccionado: " + objetos[i].name);
                }
            }
        }
    }
}
