using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaNiveles : MonoBehaviour
{
    public bool pasarNivel;
    public int indiceNivel;
    private bool isPlayerInDoorArea;

    void Start()
    {
        isPlayerInDoorArea = false;
        Debug.Log("LogicaNiveles script iniciado en objeto: " + gameObject.name);
    }

    void Update()
    {
        if (isPlayerInDoorArea && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Tecla X presionada y el jugador está en el área de la puerta.");
            TryChangeLevel();
        }

        if (pasarNivel)
        {
            Debug.Log("Pasar nivel activado.");
            TryChangeLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D llamado en objeto: " + gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInDoorArea = true;
            Debug.Log("El jugador ha entrado en el área de la puerta.");
        }
        else
        {
            Debug.Log("Colisión con un objeto que no es el jugador: " + collision.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("OnTriggerExit2D llamado en objeto: " + gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInDoorArea = false;
            Debug.Log("El jugador ha salido del área de la puerta.");
        }
        else
        {
            Debug.Log("Salida de colisión con un objeto que no es el jugador: " + collision.gameObject.name);
        }
    }

    private void TryChangeLevel()
    {
        if (PlayerInventory.instance.HasItem("SpecialItem"))
        {
            Debug.Log("Ítem especial obtenido, cambiando al nivel con índice: " + indiceNivel);
            PlayerInventory.instance.ClearSpecialItem(); // Limpiar el item especial
            CambiarNivel(indiceNivel);
        }
        else
        {
            Debug.Log("El jugador no tiene el ítem especial.");
        }
    }

    public void CambiarNivel(int indice)
    {
        Debug.Log("Cambiando al nivel con índice: " + indice);
        SceneManager.LoadScene(indice);
    }
}
