using UnityEngine;

public class LevelTest : MonoBehaviour
{
    [Header("Test Settings")]
    public bool autoStart = true;
    public float testScrollSpeed = 5f;

    private LevelManager levelManager;
    private Camera mainCamera;

    void Start()
    {
        SetupTest();

        if (autoStart)
        {
            StartLevelTest();
        }
    }

    void SetupTest()
    {
        FindOrCreateLevelManager();
        SetupCamera();
    }

    void FindOrCreateLevelManager()
    {
        levelManager = FindFirstObjectByType<LevelManager>();

        if (levelManager == null)
        {
            GameObject lmObj = new GameObject("LevelManager");
            lmObj.AddComponent<ProceduralSegmentGenerator>();
            levelManager = lmObj.AddComponent<LevelManager>();
        }
    }

    void SetupCamera()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            GameObject camObj = new GameObject("MainCamera");
            mainCamera = camObj.AddComponent<Camera>();
            camObj.tag = "MainCamera";
        }

        CameraSetup camSetup = mainCamera.GetComponent<CameraSetup>();
        if (camSetup == null)
        {
            camSetup = mainCamera.gameObject.AddComponent<CameraSetup>();
        }

        camSetup.offset = new Vector3(10, 15, -10);
        camSetup.rotation = new Vector3(35, -45, 0);
        camSetup.followTarget = false;

        mainCamera.transform.position = camSetup.offset;
        mainCamera.transform.rotation = Quaternion.Euler(camSetup.rotation);
    }

    [ContextMenu("Start Level Test")]
    public void StartLevelTest()
    {
        if (levelManager != null)
        {
            levelManager.SetScrollSpeed(testScrollSpeed);
        }
    }

    [ContextMenu("Stop Level Test")]
    public void StopLevelTest()
    {
        if (levelManager != null)
        {
            levelManager.SetScrollSpeed(0);
        }
    }

    [ContextMenu("Increase Speed")]
    public void IncreaseSpeed()
    {
        if (levelManager != null)
        {
            testScrollSpeed += 2f;
            levelManager.SetScrollSpeed(testScrollSpeed);
        }
    }

    [ContextMenu("Decrease Speed")]
    public void DecreaseSpeed()
    {
        if (levelManager != null)
        {
            testScrollSpeed = Mathf.Max(0, testScrollSpeed - 2f);
            levelManager.SetScrollSpeed(testScrollSpeed);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (levelManager != null && levelManager.GetScrollSpeed() > 0)
            {
                StopLevelTest();
            }
            else
            {
                StartLevelTest();
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            IncreaseSpeed();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DecreaseSpeed();
        }
    }
}
