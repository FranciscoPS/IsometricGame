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
        generator = GetComponent<ProceduralSegmentGenerator>();
        if (generator == null)
        {
            generator = gameObject.AddComponent<ProceduralSegmentGenerator>();
        }

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
            
            if (firstSegment.transform.position.z < -segmentLength)
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
        newSegment.transform.position = new Vector3(0, 0, nextSegmentZ);
        newSegment.transform.SetParent(transform);
        
        segments.Add(newSegment);
        
        nextSegmentZ += segmentLength;
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
