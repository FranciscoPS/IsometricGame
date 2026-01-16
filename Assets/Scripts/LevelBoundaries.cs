using UnityEngine;

public class LevelBoundaries : MonoBehaviour
{
    [Header("Horizontal Limits")]
    public float leftLimit = -9f;
    public float rightLimit = 9f;

    [Header("Visual Walls")]
    public bool showVisualWalls = false;

    private GameObject leftWall;
    private GameObject rightWall;

    void Start()
    {
        CreateBoundaryWalls();
    }

    void CreateBoundaryWalls()
    {
        leftWall = CreateWall(
            "LeftBoundary",
            new Vector3(leftLimit, 4, 50),
            new Vector3(1, 10, 100)
        );
        rightWall = CreateWall(
            "RightBoundary",
            new Vector3(rightLimit, 4, 50),
            new Vector3(1, 10, 100)
        );
    }

    GameObject CreateWall(string name, Vector3 position, Vector3 scale)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.name = name;
        wall.transform.position = position;
        wall.transform.localScale = scale;

        Renderer renderer = wall.GetComponent<Renderer>();
        if (!showVisualWalls)
        {
            renderer.enabled = false;
        }
        else
        {
            Shader shader = Shader.Find("Universal Render Pipeline/Lit");
            if (shader == null)
                shader = Shader.Find("Standard");
            Material mat = new Material(shader);
            mat.color = new Color(1, 0, 0, 0.3f);
            mat.SetFloat("_Mode", 3);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.renderQueue = 3000;
            renderer.material = mat;
        }

        wall.GetComponent<Collider>().isTrigger = false;
        wall.tag = "Boundary";

        return wall;
    }
}
