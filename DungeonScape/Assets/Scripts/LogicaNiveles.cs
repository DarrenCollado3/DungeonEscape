using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LogicaNiveles : MonoBehaviour
{
    public bool pasarNivel;
    public int indiceNivel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)) // Cambié "keyCode" a "KeyCode" para que coincida con la sintaxis correcta.
        {
            CambiarNivel(indiceNivel);
        }
        if(pasarNivel)
        {
            CambiarNivel(indiceNivel);
        }
    }

    public void CambiarNivel(int indice)
    {
        SceneManager.LoadScene(indice);
    }
}
