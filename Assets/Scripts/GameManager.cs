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

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

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
            Transform canvasTransform = FindFirstObjectByType<Canvas>()?.transform;
            if (canvasTransform != null)
            {
                Transform panelTransform = canvasTransform.Find("GameOverPanel");
                if (panelTransform != null)
                {
                    gameOverPanel = panelTransform.gameObject;
                    
                    Transform finalScoreTransform = panelTransform.Find("FinalScoreText");
                    if (finalScoreTransform != null)
                    {
                        finalScoreText = finalScoreTransform.GetComponent<TextMeshProUGUI>();
                    }
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
            return;

        isGameOver = true;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = $"Final Score: {currentScore}";
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        
        if (ReferenceManager.Instance != null)
        {
            ReferenceManager.Instance.RefreshReferences();
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
