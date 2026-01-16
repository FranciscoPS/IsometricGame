using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Segment Settings")]
    public float segmentLength = 20f;
    public int activeSegments = 5;
    public float scrollSpeed = 5f;

    [Header("Player Reference")]
    public Transform player;

    private ProceduralSegmentGenerator generator;
    private List<GameObject> segments = new List<GameObject>();
    private float nextSegmentZ = 0f;
    private int segmentCounter = 0;

    void Start()
    {
        InitializeReferences();

        nextSegmentZ = -segmentLength;
        
        for (int i = 0; i < activeSegments; i++)
        {
            SpawnSegment();
        }
    }
    
    void InitializeReferences()
    {
        if (generator == null)
        {
            generator = GetComponent<ProceduralSegmentGenerator>();
        }
        
        if (generator == null)
        {
            generator = gameObject.AddComponent<ProceduralSegmentGenerator>();
        }
        
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }
    
    public void ResetLevel()
    {
        foreach (GameObject segment in segments)
        {
            if (segment != null)
            {
                Destroy(segment);
            }
        }
        segments.Clear();
        
        nextSegmentZ = -segmentLength;
        segmentCounter = 0;
        
        InitializeReferences();
        
        for (int i = 0; i < activeSegments; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        MoveSegments();
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

            if (firstSegment.transform.position.z + segmentLength < -10)
            {
                segments.RemoveAt(0);
                Destroy(firstSegment);
                SpawnSegment();
            }
        }
    }

    void SpawnSegment()
    {
        int patternType = ChoosePattern();

        GameObject newSegment = generator.GenerateSegment(patternType);
        
        float spawnZ;
        if (segments.Count > 0)
        {
            GameObject lastSegment = segments[segments.Count - 1];
            spawnZ = lastSegment.transform.position.z + segmentLength;
        }
        else
        {
            spawnZ = nextSegmentZ;
        }
        
        newSegment.transform.position = new Vector3(0, 0, spawnZ);
        newSegment.transform.SetParent(transform);

        segments.Add(newSegment);

        segmentCounter++;
    }

    int ChoosePattern()
    {
        if (segmentCounter < 3)
        {
            return Random.Range(0, 2);
        }
        else if (segmentCounter < 10)
        {
            return Random.Range(0, 4);
        }
        else
        {
            return Random.Range(1, 7);
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
