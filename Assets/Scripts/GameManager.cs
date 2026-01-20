using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;

    [Header("Game Settings")]
    public int startingHealth = 3;
    public float gameOverDelay = 2f; // Segundos antes de volver al menú
    
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
        
        Debug.Log($"Game Over! Final Score: {currentScore}");
        
        // Volver al menú principal después de un delay
        Invoke(nameof(LoadMainMenu), gameOverDelay);
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
