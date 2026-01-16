using UnityEngine;

public class SegmentDebugger : MonoBehaviour
{
    void OnDrawGizmos()
    {
        LevelManager levelManager = FindFirstObjectByType<LevelManager>();
        if (levelManager == null) return;

        Transform levelTransform = levelManager.transform;
        
        for (int i = 0; i < levelTransform.childCount; i++)
        {
            Transform segment = levelTransform.GetChild(i);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(segment.position + new Vector3(0, 0, levelManager.segmentLength / 2), 
                               new Vector3(18, 0.1f, levelManager.segmentLength));
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(segment.position, segment.position + new Vector3(0, 5, 0));
            
            Gizmos.color = Color.red;
            Vector3 endPos = segment.position + new Vector3(0, 0, levelManager.segmentLength);
            Gizmos.DrawLine(endPos, endPos + new Vector3(0, 3, 0));
        }
    }
}
