using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI References")]
    public Button playButton;
    public Button quitButton;

    void Start()
    {
        // Asegurarse de que el tiempo está corriendo
        Time.timeScale = 1f;

        // Inicializar referencias si no están asignadas
        if (playButton == null)
        {
            GameObject playObj = GameObject.Find("PlayButton");
            if (playObj != null)
            {
                playButton = playObj.GetComponent<Button>();
            }
        }

        if (quitButton == null)
        {
            GameObject quitObj = GameObject.Find("QuitButton");
            if (quitObj != null)
            {
                quitButton = quitObj.GetComponent<Button>();
            }
        }

        // Asignar listeners
        if (playButton != null)
        {
            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(PlayGame);
        }

        if (quitButton != null)
        {
            quitButton.onClick.RemoveAllListeners();
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    public void PlayGame()
    {
        // Cargar la escena del juego (asegúrate de que esté en Build Settings)
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
