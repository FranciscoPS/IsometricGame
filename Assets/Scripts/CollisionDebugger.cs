using UnityEngine;

public class CollisionDebugger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[COLLISION] Player hit: {other.gameObject.name} | Tag: {other.tag} | IsTrigger: {other.isTrigger}");
    }
}
