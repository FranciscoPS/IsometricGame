using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform target;
    public Vector3 offset = new Vector3(10, 15, -10);
    public Vector3 rotation = new Vector3(35, -45, 0);
    public bool followTarget = false;

    void Start()
    {
        // Configurar rotación isométrica
        transform.rotation = Quaternion.Euler(rotation);

        if (target != null && followTarget)
        {
            transform.position = target.position + offset;
        }
        else
        {
            transform.position = offset;
        }
    }

    void LateUpdate()
    {
        if (target != null && followTarget)
        {
            // Seguir al jugador manteniendo el offset
            Vector3 desiredPosition = target.position + offset;
            transform.position = desiredPosition;
        }
    }
}
