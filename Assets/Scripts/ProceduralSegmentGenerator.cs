using System.Collections.Generic;
using UnityEngine;

public class ProceduralSegmentGenerator : MonoBehaviour
{
    [System.Serializable]
    public class SegmentPattern
    {
        public string patternName;
        public int difficulty;
    }

    [Header("Segment Settings")]
    public float segmentLength = 20f;
    public float segmentWidth = 18f;

    [Header("Materials")]
    public Material groundMaterial;
    public Material wallMaterial;
    public Material towerMaterial;
    public Material ceilingMaterial;

    private List<Material> materials = new List<Material>();

    void Start()
    {
        CreateDefaultMaterials();
    }

    void CreateDefaultMaterials()
    {
        if (groundMaterial == null)
        {
            groundMaterial = CreateMaterial(new Color(0.2f, 0.4f, 0.2f));
        }

        if (wallMaterial == null)
        {
            wallMaterial = CreateMaterial(new Color(0.2f, 0.8f, 0.2f));
        }

        if (towerMaterial == null)
        {
            towerMaterial = CreateMaterial(new Color(0.8f, 0.2f, 0.2f));
        }

        if (ceilingMaterial == null)
        {
            ceilingMaterial = CreateMaterial(new Color(0.2f, 0.2f, 0.5f));
        }
    }

    Material CreateMaterial(Color color)
    {
        // Intentar shaders de URP primero, luego fallbacks
        Shader shader = Shader.Find("Universal Render Pipeline/Simple Lit");
        if (shader == null)
            shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null)
            shader = Shader.Find("Standard");
        if (shader == null)
            shader = Shader.Find("Diffuse");
        if (shader == null)
            shader = Shader.Find("Unlit/Color");

        Material mat = new Material(shader != null ? shader : Shader.Find("Sprites/Default"));
        mat.color = color;
        return mat;
    }

    public GameObject GenerateSegment(int patternType)
    {
        CreateDefaultMaterials();

        GameObject segment = new GameObject($"Segment_{patternType}");

        CreateGround(segment.transform);

        switch (patternType)
        {
            case 0:
                CreatePattern_Empty(segment.transform);
                break;
            case 1:
                CreatePattern_SimpleTowers(segment.transform);
                break;
            case 2:
                CreatePattern_LowWalls(segment.transform);
                break;
            case 3:
                CreatePattern_FloatingBlocks(segment.transform);
                break;
            case 4:
                CreatePattern_Tunnel(segment.transform);
                break;
            case 5:
                CreatePattern_Mixed(segment.transform);
                break;
            case 6:
                CreatePattern_Zigzag(segment.transform);
                break;
            default:
                CreatePattern_Random(segment.transform);
                break;
        }

        return segment;
    }

    void CreateGround(Transform parent)
    {
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ground.name = "Ground";
        ground.transform.SetParent(parent);
        ground.transform.localPosition = new Vector3(0, -0.5f, segmentLength / 2);
        ground.transform.localScale = new Vector3(segmentWidth, 1f, segmentLength);
        ground.GetComponent<Renderer>().material = groundMaterial;
        ground.GetComponent<Collider>().isTrigger = false;
    }

    void CreatePattern_Empty(Transform parent) { }

    void CreatePattern_SimpleTowers(Transform parent)
    {
        CreateTower(parent, new Vector3(-6, 0, 8));
        CreateTower(parent, new Vector3(6, 0, 15));
    }

    void CreatePattern_LowWalls(Transform parent)
    {
        CreateWall(parent, new Vector3(-4, 1, 5), new Vector3(3, 2, 2));
        CreateWall(parent, new Vector3(4, 1, 12), new Vector3(3, 2, 2));
        CreateWall(parent, new Vector3(0, 1, 18), new Vector3(4, 2, 2));
        CreateTower(parent, new Vector3(-7, 0, 10));
    }

    void CreatePattern_FloatingBlocks(Transform parent)
    {
        CreateFloatingBlock(parent, new Vector3(-5, 3, 5), new Vector3(2, 2, 2));
        CreateFloatingBlock(parent, new Vector3(3, 6, 10), new Vector3(2, 2, 2));
        CreateFloatingBlock(parent, new Vector3(-2, 8, 15), new Vector3(3, 2, 2));
        CreateFloatingBlock(parent, new Vector3(6, 4.5f, 12), new Vector3(2, 2, 2));
    }

    void CreatePattern_Tunnel(Transform parent)
    {
        // Patrón cambiado - solo torres, sin techos
        CreateTower(parent, new Vector3(-6, 0, 5));
        CreateTower(parent, new Vector3(6, 0, 10));
        CreateTower(parent, new Vector3(0, 0, 15));
    }

    void CreatePattern_Mixed(Transform parent)
    {
        CreateTower(parent, new Vector3(-6, 0, 5));
        CreateWall(parent, new Vector3(3, 1, 8), new Vector3(2, 2, 2));
        CreateFloatingBlock(parent, new Vector3(-2, 5, 12), new Vector3(2, 2, 2));
        CreateFloatingBlock(parent, new Vector3(4, 7, 14), new Vector3(2, 2, 2));
        CreateTower(parent, new Vector3(5, 0, 16));
    }

    void CreatePattern_Zigzag(Transform parent)
    {
        CreateFloatingBlock(parent, new Vector3(-5, 2.5f, 5), new Vector3(4, 2, 2));
        CreateFloatingBlock(parent, new Vector3(5, 6, 10), new Vector3(4, 2, 2));
        CreateFloatingBlock(parent, new Vector3(-5, 4, 15), new Vector3(4, 2, 2));
        CreateFloatingBlock(parent, new Vector3(2, 7.5f, 12), new Vector3(3, 2, 2));
    }

    void CreatePattern_Random(Transform parent)
    {
        int numObstacles = Random.Range(2, 5);
        for (int i = 0; i < numObstacles; i++)
        {
            float z = Random.Range(5f, segmentLength - 2);
            float x = Random.Range(-6f, 6f);

            int obstacleType = Random.Range(0, 3);
            if (obstacleType == 0)
            {
                CreateTower(parent, new Vector3(x, 0, z));
            }
            else if (obstacleType == 1)
            {
                CreateWall(parent, new Vector3(x, 1, z), new Vector3(2, 2, 2));
            }
            else
            {
                float y = Random.Range(2.5f, 8f);
                CreateFloatingBlock(parent, new Vector3(x, y, z), new Vector3(2, 2, 2));
            }
        }
    }

    void CreateTower(Transform parent, Vector3 localPos)
    {
        GameObject tower = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        tower.name = "Tower";
        tower.transform.SetParent(parent);
        tower.transform.localPosition = localPos;
        tower.transform.localScale = new Vector3(1.5f, 2f, 1.5f);
        tower.GetComponent<Renderer>().material = towerMaterial;
        tower.tag = "Enemy";
        
        CapsuleCollider col = tower.GetComponent<CapsuleCollider>();
        col.isTrigger = true;
        
        EnemyTurret turret = tower.AddComponent<EnemyTurret>();
        Destructible destructible = tower.AddComponent<Destructible>();
        destructible.maxHealth = 3;
        destructible.scoreValue = 50;
    }

    void CreateWall(Transform parent, Vector3 localPos, Vector3 scale)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.name = "Wall";
        wall.transform.SetParent(parent);
        wall.transform.localPosition = localPos;
        wall.transform.localScale = scale;
        wall.GetComponent<Renderer>().material = wallMaterial;

        wall.tag = "Obstacle";
        wall.GetComponent<Collider>().isTrigger = true;
        
        // Los muros verdes son indestructibles - no tienen componente Destructible
    }

    void CreateFloatingBlock(Transform parent, Vector3 localPos, Vector3 scale)
    {
        GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
        block.name = "FloatingBlock";
        block.transform.SetParent(parent);
        block.transform.localPosition = localPos;
        block.transform.localScale = scale;

        Material floatingMat = CreateMaterial(new Color(0.6f, 0.4f, 0.2f));
        block.GetComponent<Renderer>().material = floatingMat;

        block.tag = "Obstacle";
        block.GetComponent<Collider>().isTrigger = true;
        
        Destructible destructible = block.AddComponent<Destructible>();
        destructible.maxHealth = 2;
        destructible.scoreValue = 15;
    }

    void CreateCeiling(Transform parent, Vector3 localPos, Vector3 scale)
    {
        GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ceiling.name = "Ceiling";
        ceiling.transform.SetParent(parent);
        ceiling.transform.localPosition = localPos;
        ceiling.transform.localScale = scale;
        ceiling.GetComponent<Renderer>().material = ceilingMaterial;

        ceiling.tag = "Obstacle";
        ceiling.GetComponent<Collider>().isTrigger = true;
    }
}
