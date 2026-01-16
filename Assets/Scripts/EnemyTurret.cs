using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public float fireRate = 2f;
    public float detectionRange = 15f;
    public float bulletSpeed = 10f;

    private Transform player;
    private float nextFireTime;
    private GameObject firePoint;

    void Start()
    {
        // Buscar al jugador
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        // Crear firePoint
        firePoint = new GameObject("FirePoint");
        firePoint.transform.SetParent(transform);
        firePoint.transform.localPosition = new Vector3(0, 1, 0);

        // Crear prefab de bala si no existe
        if (bulletPrefab == null)
        {
            bulletPrefab = CreateEnemyBulletPrefab();
        }
    }

    void Update()
    {
        if (player != null && Time.time >= nextFireTime)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            // Solo disparar si el jugador está en rango y adelante de la torreta
            if (distance < detectionRange && player.position.z < transform.position.z)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null && player != null)
        {
            GameObject bullet = Instantiate(
                bulletPrefab,
                firePoint.transform.position,
                Quaternion.identity
            );

            // Calcular dirección hacia el jugador
            Vector3 direction = (player.position - firePoint.transform.position).normalized;

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = direction * bulletSpeed;
            }

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.isPlayerBullet = false;
                bulletScript.speed = bulletSpeed;
            }

            bullet.tag = "EnemyBullet";
        }
    }

    GameObject CreateEnemyBulletPrefab()
    {
        GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bullet.name = "EnemyBullet";
        bullet.transform.localScale = Vector3.one * 0.3f;

        // Material rojo
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null)
            shader = Shader.Find("Standard");
        Material mat = new Material(shader);
        mat.color = Color.red;
        bullet.GetComponent<Renderer>().material = mat;

        // Physics
        Rigidbody rb = bullet.AddComponent<Rigidbody>();
        rb.useGravity = false;

        SphereCollider collider = bullet.GetComponent<SphereCollider>();
        collider.isTrigger = true;

        // Script
        Bullet bulletScript = bullet.AddComponent<Bullet>();
        bulletScript.isPlayerBullet = false;
        bulletScript.damage = 1;

        bullet.tag = "EnemyBullet";

        // Desactivar para usar como prefab
        bullet.SetActive(false);

        return bullet;
    }

    void OnDrawGizmosSelected()
    {
        // Visualizar rango de detección
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
