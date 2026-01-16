using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    private static GameObject playerBulletPrefab;
    private static GameObject enemyBulletPrefab;

    public static GameObject CreatePlayerBullet()
    {
        if (playerBulletPrefab == null)
        {
            playerBulletPrefab = CreateBulletPrefab("PlayerBullet", Color.cyan, true);
        }

        GameObject bullet = Instantiate(playerBulletPrefab);
        bullet.SetActive(true);
        return bullet;
    }

    public static GameObject CreateEnemyBullet()
    {
        if (enemyBulletPrefab == null)
        {
            enemyBulletPrefab = CreateBulletPrefab("EnemyBullet", Color.red, false);
        }

        GameObject bullet = Instantiate(enemyBulletPrefab);
        bullet.SetActive(true);
        return bullet;
    }

    static GameObject CreateBulletPrefab(string name, Color color, bool isPlayerBullet)
    {
        GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bullet.name = name;
        bullet.transform.localScale = Vector3.one * 0.3f;

        // Material
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null) shader = Shader.Find("Standard");
        Material mat = new Material(shader);
        mat.color = color;
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", color * 0.5f);
        bullet.GetComponent<Renderer>().material = mat;

        // Physics
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = bullet.AddComponent<Rigidbody>();
        }
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Collider
        SphereCollider collider = bullet.GetComponent<SphereCollider>();
        collider.isTrigger = true;

        // Script
        Bullet bulletScript = bullet.AddComponent<Bullet>();
        bulletScript.isPlayerBullet = isPlayerBullet;
        bulletScript.speed = 20f;
        bulletScript.lifetime = 5f;
        bulletScript.damage = 1;

        // Tag
        bullet.tag = isPlayerBullet ? "PlayerBullet" : "EnemyBullet";

        // Desactivar para usar como prefab
        bullet.SetActive(false);

        return bullet;
    }

    public static GameObject GetPlayerBulletPrefab()
    {
        if (playerBulletPrefab == null)
        {
            playerBulletPrefab = CreateBulletPrefab("PlayerBullet", Color.cyan, true);
        }
        return playerBulletPrefab;
    }

    public static GameObject GetEnemyBulletPrefab()
    {
        if (enemyBulletPrefab == null)
        {
            enemyBulletPrefab = CreateBulletPrefab("EnemyBullet", Color.red, false);
        }
        return enemyBulletPrefab;
    }
}
