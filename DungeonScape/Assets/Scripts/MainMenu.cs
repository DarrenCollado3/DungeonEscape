using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Importa la biblioteca de UI para manejar los botones

public class SceneController : MonoBehaviour
{
    [SerializeField] private int initialSceneIndex = 0;
    private int currentSceneIndex;

    [SerializeField] private Button nextButton;  // Botón para cargar la siguiente escena
    [SerializeField] private Button quitButton;  // Botón para salir del juego

    void Start()
    {
        currentSceneIndex = initialSceneIndex;

        // Asegúrate de que los botones están configurados correctamente
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(LoadNextScene);
        }
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitApplication);
        }

        // Cargar la escena inicial si no es la escena actualmente activa
        if (SceneManager.GetActiveScene().buildIndex != currentSceneIndex)
        {
            SceneManager.LoadScene(currentSceneIndex);
        }
    }

    public void LoadNextScene()
    {
        Debug.Log("Loading next scene");
        currentSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void QuitApplication()
    {
        Debug.Log("Quitting application");
        // If we are running in a standalone build of the game
#if UNITY_STANDALONE
        Application.Quit();
#endif

        // If we are running in the editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
