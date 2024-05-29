using UnityEngine;
using UnityEngine.UI;

public class SceneController2 : MonoBehaviour
{
    [SerializeField] private Button quitButton;  // Botón para salir del juego

    void Start()
    {
        // Asegúrate de que el botón de salida está configurado correctamente
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitApplication2);
        }
    }

    public void QuitApplication2()
    {
        Debug.Log("Quitting application");
        // Si estamos ejecutando en una compilación independiente del juego
#if UNITY_STANDALONE
        Application.Quit();
#endif

        // Si estamos ejecutando en el editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
