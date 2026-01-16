using UnityEngine;
using TMPro;

public class GameSetup : MonoBehaviour
{
    [Header("Setup Options")]
    public bool autoSetupScene = true;

    void Start()
    {
        if (autoSetupScene)
        {
            SetupCompleteScene();
        }
    }

    [ContextMenu("Setup Complete Scene")]
    public void SetupCompleteScene()
    {
        GameObject player = SetupPlayer();
        SetupCamera(player.transform);
        SetupLevelManager();
        SetupBoundaries();
        SetupUI();
        SetupGameManager();
        SetupLighting();
    }

    GameObject SetupPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            player = GameObject.CreatePrimitive(PrimitiveType.Cube);
            player.name = "Player";
            player.tag = "Player";
            player.transform.position = new Vector3(0, 3, 0);
            player.transform.localScale = new Vector3(1, 0.5f, 1.5f);

            Shader shader = Shader.Find("Universal Render Pipeline/Lit");
            if (shader == null)
                shader = Shader.Find("Standard");
            Material mat = new Material(shader);
            mat.color = Color.blue;
            player.GetComponent<Renderer>().material = mat;

            BoxCollider collider = player.GetComponent<BoxCollider>();
            collider.isTrigger = true;

            player.AddComponent<PlayerController>();

            PlayerController controller = player.GetComponent<PlayerController>();
            controller.bulletPrefab = BulletFactory.GetPlayerBulletPrefab();
        }

        return player;
    }

    void SetupCamera(Transform target)
    {
        Camera mainCam = Camera.main;
        if (mainCam == null)
        {
            GameObject camObj = new GameObject("Main Camera");
            mainCam = camObj.AddComponent<Camera>();
            camObj.tag = "MainCamera";
        }

        CameraSetup camSetup = mainCam.GetComponent<CameraSetup>();
        if (camSetup == null)
        {
            camSetup = mainCam.gameObject.AddComponent<CameraSetup>();
        }

        camSetup.target = target;
        camSetup.offset = new Vector3(10, 15, -10);
        camSetup.rotation = new Vector3(35, -45, 0);
        camSetup.followTarget = false;

        mainCam.transform.position = camSetup.offset;
        mainCam.transform.rotation = Quaternion.Euler(camSetup.rotation);
    }

    void SetupLevelManager()
    {
        GameObject levelManagerObj = GameObject.Find("LevelManager");
        if (levelManagerObj == null)
        {
            levelManagerObj = new GameObject("LevelManager");
        }

        if (levelManagerObj.GetComponent<ProceduralSegmentGenerator>() == null)
        {
            levelManagerObj.AddComponent<ProceduralSegmentGenerator>();
        }

        if (levelManagerObj.GetComponent<LevelManager>() == null)
        {
            LevelManager lm = levelManagerObj.AddComponent<LevelManager>();

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                lm.player = player.transform;
            }
        }
    }

    void SetupBoundaries()
    {
        GameObject boundariesObj = GameObject.Find("Boundaries");
        if (boundariesObj == null)
        {
            boundariesObj = new GameObject("Boundaries");
        }

        if (boundariesObj.GetComponent<LevelBoundaries>() == null)
        {
            boundariesObj.AddComponent<LevelBoundaries>();
        }
    }

    void SetupUI()
    {
        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("Canvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
            canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
        }

        CreateUIText(
            canvas.transform,
            "ScoreText",
            new Vector2(10, -10),
            new Vector2(200, 30),
            TextAnchor.UpperLeft,
            "Score: 0"
        );

        CreateUIText(
            canvas.transform,
            "HealthText",
            new Vector2(10, -50),
            new Vector2(200, 30),
            TextAnchor.UpperLeft,
            "Health: 3"
        );

        CreateHeightIndicator(canvas.transform);
        CreateGameOverPanel(canvas.transform);
    }

    void CreateUIText(
        Transform parent,
        string name,
        Vector2 anchoredPosition,
        Vector2 sizeDelta,
        TextAnchor alignment,
        string text
    )
    {
        Transform existing = parent.Find(name);
        if (existing != null)
            return;

        GameObject textObj = new GameObject(name);
        textObj.transform.SetParent(parent);

        RectTransform rect = textObj.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.pivot = new Vector2(0, 1);
        rect.anchoredPosition = anchoredPosition;
        rect.sizeDelta = sizeDelta;

        UnityEngine.UI.Text textComponent = textObj.AddComponent<UnityEngine.UI.Text>();
        textComponent.text = text;
        textComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        textComponent.fontSize = 20;
        textComponent.color = Color.white;
        textComponent.alignment = alignment;
    }

    void CreateHeightIndicator(Transform parent)
    {
        Transform existing = parent.Find("HeightIndicator");
        if (existing != null)
            return;

        GameObject indicatorObj = new GameObject("HeightIndicator");
        indicatorObj.transform.SetParent(parent);

        RectTransform rect = indicatorObj.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(1, 0.5f);
        rect.anchorMax = new Vector2(1, 0.5f);
        rect.pivot = new Vector2(1, 0.5f);
        rect.anchoredPosition = new Vector2(-30, 0);
        rect.sizeDelta = new Vector2(30, 200);

        UnityEngine.UI.Slider slider = indicatorObj.AddComponent<UnityEngine.UI.Slider>();
        slider.direction = UnityEngine.UI.Slider.Direction.BottomToTop;

        GameObject bg = new GameObject("Background");
        bg.transform.SetParent(indicatorObj.transform);
        RectTransform bgRect = bg.AddComponent<RectTransform>();
        bgRect.anchorMin = Vector2.zero;
        bgRect.anchorMax = Vector2.one;
        bgRect.sizeDelta = Vector2.zero;
        UnityEngine.UI.Image bgImage = bg.AddComponent<UnityEngine.UI.Image>();
        bgImage.color = new Color(0.2f, 0.2f, 0.2f, 0.5f);


        GameObject fillArea = new GameObject("Fill Area");
        fillArea.transform.SetParent(indicatorObj.transform);
        RectTransform fillAreaRect = fillArea.AddComponent<RectTransform>();
        fillAreaRect.anchorMin = Vector2.zero;
        fillAreaRect.anchorMax = Vector2.one;
        fillAreaRect.sizeDelta = Vector2.zero;

        GameObject fill = new GameObject("Fill");
        fill.transform.SetParent(fillArea.transform);
        RectTransform fillRect = fill.AddComponent<RectTransform>();
        fillRect.anchorMin = Vector2.zero;
        fillRect.anchorMax = Vector2.one;
        fillRect.sizeDelta = Vector2.zero;
        UnityEngine.UI.Image fillImage = fill.AddComponent<UnityEngine.UI.Image>();
        fillImage.color = Color.green;

        slider.fillRect = fillRect;
        slider.targetGraphic = fillImage;

        HeightIndicator heightScript = indicatorObj.AddComponent<HeightIndicator>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            heightScript.player = player.transform;
        }
        heightScript.heightSlider = slider;
    }

    void CreateGameOverPanel(Transform parent)
    {
        Transform existing = parent.Find("GameOverPanel");
        if (existing != null)
            return;

        GameObject panel = new GameObject("GameOverPanel");
        panel.transform.SetParent(parent);

        RectTransform rect = panel.AddComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;

        UnityEngine.UI.Image image = panel.AddComponent<UnityEngine.UI.Image>();
        image.color = new Color(0, 0, 0, 0.8f);

        CreateUIText(
            panel.transform,
            "GameOverText",
            new Vector2(0, 50),
            new Vector2(400, 60),
            TextAnchor.MiddleCenter,
            "GAME OVER"
        );
        GameObject goText = panel.transform.Find("GameOverText").gameObject;
        RectTransform goRect = goText.GetComponent<RectTransform>();
        goRect.anchorMin = new Vector2(0.5f, 0.5f);
        goRect.anchorMax = new Vector2(0.5f, 0.5f);
        goRect.pivot = new Vector2(0.5f, 0.5f);
        goText.GetComponent<UnityEngine.UI.Text>().fontSize = 40;

        CreateUIText(
            panel.transform,
            "FinalScoreText",
            new Vector2(0, -20),
            new Vector2(400, 40),
            TextAnchor.MiddleCenter,
            "Final Score: 0"
        );
        GameObject scoreText = panel.transform.Find("FinalScoreText").gameObject;
        RectTransform scoreRect = scoreText.GetComponent<RectTransform>();
        scoreRect.anchorMin = new Vector2(0.5f, 0.5f);
        scoreRect.anchorMax = new Vector2(0.5f, 0.5f);
        scoreRect.pivot = new Vector2(0.5f, 0.5f);
        scoreText.GetComponent<UnityEngine.UI.Text>().fontSize = 24;

        CreateButton(panel.transform, "RestartButton", new Vector2(0, -100), "RESTART");

        panel.SetActive(false);
    }

    void CreateButton(Transform parent, string name, Vector2 anchoredPosition, string text)
    {
        GameObject buttonObj = new GameObject(name);
        buttonObj.transform.SetParent(parent);

        RectTransform rect = buttonObj.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = anchoredPosition;
        rect.sizeDelta = new Vector2(200, 50);

        UnityEngine.UI.Image image = buttonObj.AddComponent<UnityEngine.UI.Image>();
        image.color = new Color(0.2f, 0.6f, 0.2f);

        UnityEngine.UI.Button button = buttonObj.AddComponent<UnityEngine.UI.Button>();

        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(buttonObj.transform);

        RectTransform textRect = textObj.AddComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.sizeDelta = Vector2.zero;

        UnityEngine.UI.Text textComponent = textObj.AddComponent<UnityEngine.UI.Text>();
        textComponent.text = text;
        textComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        textComponent.fontSize = 24;
        textComponent.color = Color.white;
        textComponent.alignment = TextAnchor.MiddleCenter;
    }

    void SetupGameManager()
    {
        GameObject gmObj = GameObject.Find("GameManager");
        if (gmObj == null)
        {
            gmObj = new GameObject("GameManager");
        }

        GameManager gm = gmObj.GetComponent<GameManager>();
        if (gm == null)
        {
            gm = gmObj.AddComponent<GameManager>();
        }

        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas != null)
        {
            Transform scoreText = canvas.transform.Find("ScoreText");
            if (scoreText != null)
            {
                gm.scoreText = scoreText.GetComponent<TextMeshProUGUI>();
            }

            Transform healthText = canvas.transform.Find("HealthText");
            if (healthText != null)
            {
                gm.healthText = healthText.GetComponent<TextMeshProUGUI>();
            }

            Transform gameOverPanel = canvas.transform.Find("GameOverPanel");
            if (gameOverPanel != null)
            {
                gm.gameOverPanel = gameOverPanel.gameObject;

                Transform finalScoreText = gameOverPanel.Find("FinalScoreText");
                if (finalScoreText != null)
                {
                    gm.finalScoreText = finalScoreText.GetComponent<TextMeshProUGUI>();
                }

                Transform restartButton = gameOverPanel.Find("RestartButton");
                if (restartButton != null)
                {
                    UnityEngine.UI.Button button =
                        restartButton.GetComponent<UnityEngine.UI.Button>();
                    button.onClick.AddListener(gm.RestartGame);
                }
            }
        }
    }

    void SetupLighting()
    {
        Light[] lights = FindObjectsOfType<Light>();
        Light directionalLight = null;

        foreach (Light light in lights)
        {
            if (light.type == LightType.Directional)
            {
                directionalLight = light;
                break;
            }
        }

        if (directionalLight == null)
        {
            GameObject lightObj = new GameObject("Directional Light");
            directionalLight = lightObj.AddComponent<Light>();
            directionalLight.type = LightType.Directional;
        }

        // Configurar sombras verticales (directamente encima)
        directionalLight.transform.rotation = Quaternion.Euler(90, 0, 0);
        directionalLight.shadows = LightShadows.Soft;
        directionalLight.shadowStrength = 0.8f;

        Debug.Log("Lighting configured: Shadows set to vertical (90° downward)");
    }
}
