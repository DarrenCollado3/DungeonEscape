using UnityEngine;
using UnityEngine.UI;

public class SceneController2 : MonoBehaviour
{
    [SerializeField] private Button quitButton;  // Bot�n para salir del juego

    void Start()
    {
        // Aseg�rate de que el bot�n de salida est� configurado correctamente
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitApplication2);
        }
    }

    public void QuitApplication2()
    {
        Debug.Log("Quitting application");
        // Si estamos ejecutando en una compilaci�n independiente del juego
#if UNITY_STANDALONE
        Application.Quit();
#endif

        // Si estamos ejecutando en el editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
