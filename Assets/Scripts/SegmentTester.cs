using UnityEngine;

public class SegmentTester : MonoBehaviour
{
    [Header("Test Settings")]
    [Tooltip("Genera todos los patrones al iniciar")]
    public bool generateAllPatternsOnStart = true;
    
    [Tooltip("Espaciado entre segmentos cuando se generan todos")]
    public float spacing = 25f;

    [Header("Manual Testing")]
    [Tooltip("Patrón a generar manualmente (0-7)")]
    [Range(0, 7)]
    public int patternToGenerate = 0;

    private ProceduralSegmentGenerator generator;

    void Start()
    {
        // Obtener o crear el generador
        generator = GetComponent<ProceduralSegmentGenerator>();
        if (generator == null)
        {
            generator = gameObject.AddComponent<ProceduralSegmentGenerator>();
        }

        if (generateAllPatternsOnStart)
        {
            GenerateAllPatterns();
        }
    }

    [ContextMenu("Generate All Patterns")]
    public void GenerateAllPatterns()
    {
        Debug.Log("Generando todos los patrones de segmentos...");
        
        // Limpiar segmentos anteriores
        ClearAllSegments();

        // Generar cada patrón
        for (int i = 0; i < 8; i++)
        {
            GameObject segment = generator.GenerateSegment(i);
            segment.transform.position = new Vector3(0, 0, i * spacing);
            segment.transform.SetParent(transform);
            segment.name = $"Segment_{i}_{GetPatternName(i)}";
            
            Debug.Log($"✓ Generado: {segment.name}");
        }

        Debug.Log("¡Todos los patrones generados! Mueve la cámara para verlos.");
    }

    [ContextMenu("Generate Single Pattern")]
    public void GenerateSinglePattern()
    {
        GameObject segment = generator.GenerateSegment(patternToGenerate);
        segment.transform.position = new Vector3(0, 0, 0);
        segment.transform.SetParent(transform);
        segment.name = $"Segment_{GetPatternName(patternToGenerate)}";
    }

    [ContextMenu("Clear All Segments")]
    public void ClearAllSegments()
    {
        Debug.Log("Limpiando segmentos...");
        
        // Destruir todos los hijos
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        
        Debug.Log("✓ Segmentos limpiados");
    }

    [ContextMenu("Generate Random Pattern")]
    public void GenerateRandomPattern()
    {
        int randomPattern = Random.Range(0, 8);
        
        GameObject segment = generator.GenerateSegment(randomPattern);
        segment.transform.position = new Vector3(0, 0, 0);
        segment.transform.SetParent(transform);
        segment.name = $"Segment_{GetPatternName(randomPattern)}";
    }

    string GetPatternName(int patternType)
    {
        switch (patternType)
        {
            case 0: return "Empty";
            case 1: return "SimpleTowers";
            case 2: return "LowWalls";
            case 3: return "FloatingBlocks";
            case 4: return "Tunnel";
            case 5: return "Mixed";
            case 6: return "Zigzag";
            case 7: return "Random";
            default: return "Unknown";
        }
    }
}
