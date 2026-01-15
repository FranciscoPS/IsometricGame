using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [Header("Segment Settings")]
    public float segmentLength = 20f;
    public int activeSegments = 3;
    public float scrollSpeed = 5f;

    [Header("Player Reference")]
    public Transform player;

    private ProceduralSegmentGenerator generator;
    private List<GameObject> segments = new List<GameObject>();
    private float nextSegmentZ = 0f;
    private int segmentCounter = 0;

    void Start()
    {
        // Obtener o crear generador
        generator = GetComponent<ProceduralSegmentGenerator>();
        if (generator == null)
        {
            generator = gameObject.AddComponent<ProceduralSegmentGenerator>();
        }

        // Generar segmentos iniciales
        for (int i = 0; i < activeSegments; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        // Mover segmentos hacia el jugador
        MoveSegments();

        // Reciclar segmentos que pasaron
        RecycleSegments();
    }

    void MoveSegments()
    {
        foreach (GameObject segment in segments)
        {
            segment.transform.position += Vector3.back * scrollSpeed * Time.deltaTime;
        }
    }

    void RecycleSegments()
    {
        if (segments.Count > 0)
        {
            GameObject firstSegment = segments[0];
            
            // Si el segmento pasó completamente al jugador
            if (firstSegment.transform.position.z < -segmentLength)
            {
                // Destruir y remover de la lista
                segments.RemoveAt(0);
                Destroy(firstSegment);

                // Crear nuevo segmento adelante
                SpawnSegment();
            }
        }
    }

    void SpawnSegment()
    {
        // Elegir patrón según progresión
        int patternType = ChoosePattern();
        
        GameObject newSegment = generator.GenerateSegment(patternType);
        newSegment.transform.position = new Vector3(0, 0, nextSegmentZ);
        newSegment.transform.SetParent(transform);
        
        segments.Add(newSegment);
        
        nextSegmentZ += segmentLength;
        segmentCounter++;
    }

    int ChoosePattern()
    {
        // Los primeros segmentos son más fáciles
        if (segmentCounter < 3)
        {
            return Random.Range(0, 2); // Solo vacío o torres simples
        }
        else if (segmentCounter < 10)
        {
            return Random.Range(0, 4); // Patrones fáciles a medios
        }
        else
        {
            return Random.Range(1, 7); // Todos los patrones (excepto vacío)
        }
    }

    public void SetScrollSpeed(float speed)
    {
        scrollSpeed = speed;
    }

    public float GetScrollSpeed()
    {
        return scrollSpeed;
    }
}
