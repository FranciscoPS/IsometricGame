using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    private static ReferenceManager instance;
    public static ReferenceManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<ReferenceManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("ReferenceManager");
                    instance = obj.AddComponent<ReferenceManager>();
                }
            }
            return instance;
        }
    }
    
    private LevelManager levelManager;
    private ProceduralSegmentGenerator segmentGenerator;
    private GameManager gameManager;
    private Transform player;
    private Camera mainCamera;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        RefreshReferences();
    }
    
    public void RefreshReferences()
    {
        levelManager = null;
        segmentGenerator = null;
        gameManager = null;
        player = null;
        mainCamera = null;
    }
    
    public LevelManager GetLevelManager()
    {
        if (levelManager == null)
        {
            levelManager = FindFirstObjectByType<LevelManager>();
        }
        return levelManager;
    }
    
    public ProceduralSegmentGenerator GetSegmentGenerator()
    {
        if (segmentGenerator == null)
        {
            segmentGenerator = FindFirstObjectByType<ProceduralSegmentGenerator>();
        }
        return segmentGenerator;
    }
    
    public GameManager GetGameManager()
    {
        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }
        return gameManager;
    }
    
    public Transform GetPlayer()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
        return player;
    }
    
    public Camera GetMainCamera()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        return mainCamera;
    }
    
    void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        RefreshReferences();
    }
}
