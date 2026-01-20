using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public Button restartButton;
    public Button mainMenuButton;

    [Header("Game Settings")]
    public int startingHealth = 3;
    
    private int currentScore = 0;
    private int currentHealth;
    private bool isGameOver = false;

    [Header("Speed Progression")]
    public float startSpeed = 5f;
    public float speedIncreaseRate = 0.5f;
    public float maxSpeed = 15f;
    private float currentSpeed;

    private LevelManager levelManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeReferences();
        
        currentHealth = startingHealth;
        currentSpeed = startSpeed;
        isGameOver = false;

        UpdateUI();
    }
    
    void InitializeReferences()
    {
        if (levelManager == null)
        {
            levelManager = FindFirstObjectByType<LevelManager>();
        }
        
        if (scoreText == null)
        {
            GameObject scoreObj = GameObject.Find("ScoreText");
            if (scoreObj != null)
            {
                scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
            }
        }
        
        if (healthText == null)
        {
            GameObject healthObj = GameObject.Find("HealthText");
            if (healthObj != null)
            {
                healthText = healthObj.GetComponent<TextMeshProUGUI>();
            }
        }
        
        if (gameOverPanel == null)
        {
            Canvas canvas = FindFirstObjectByType<Canvas>();
            if (canvas != null)
            {
                Transform panel = canvas.transform.Find("GameOverPanel");
                if (panel != null)
                {
                    gameOverPanel = panel.gameObject;
                    
                    Transform finalScore = panel.Find("FinalScoreText");
                    if (finalScore != null)
                    {
                        finalScoreText = finalScore.GetComponent<TextMeshProUGUI>();
                        if (finalScoreText == null)
                        {
                            finalScoreText = finalScore.GetComponent<TMPro.TextMeshProUGUI>();
                        }
                    }
                    
                    Transform restart = panel.Find("RestartButton");
                    if (restart != null)
                    {
                        restartButton = restart.GetComponent<Button>();
                        if (restartButton != null)
                        {
                            restartButton.onClick.RemoveAllListeners();
                            restartButton.onClick.AddListener(RestartGame);
                        }
                    }
                    
                    Transform mainMenu = panel.Find("MainMenuButton");
                    if (mainMenu != null)
                    {
                        mainMenuButton = mainMenu.GetComponent<Button>();
                        if (mainMenuButton != null)
                        {
                            mainMenuButton.onClick.RemoveAllListeners();
                            mainMenuButton.onClick.AddListener(LoadMainMenu);
                        }
                    }
                    
                    gameOverPanel.SetActive(false);
                }
            }
        }
    }

    void Update()
    {
        if (!isGameOver)
        {
            IncreaseSpeed();
        }
    }

    void IncreaseSpeed()
    {
        if (levelManager != null && currentSpeed < maxSpeed)
        {
            currentSpeed += speedIncreaseRate * Time.deltaTime;
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
            levelManager.SetScrollSpeed(currentSpeed);
        }
    }

    public void AddScore(int points)
    {
        if (!isGameOver)
        {
            currentScore += points;
            UpdateUI();
        }
    }

    public void UpdateHealth(int health)
    {
        currentHealth = health;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
        }

        if (healthText != null)
        {
            healthText.text = $"Health: {currentHealth}";
        }
    }

    public void GameOver()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;
        
        // Desactivar input del jugador
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            player.DisableInput();
        }
        
        if (finalScoreText != null)
        {
            finalScoreText.text = $"Final Score: {currentScore}";
        }
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        currentScore = 0;
        currentHealth = startingHealth;
        currentSpeed = startSpeed;
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        
        if (levelManager != null)
        {
            levelManager.ResetLevel();
            levelManager.SetScrollSpeed(startSpeed);
        }
        
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            player.ResetPlayer();
            player.EnableInput();
        }
        
        UpdateUI();
    }

    void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
