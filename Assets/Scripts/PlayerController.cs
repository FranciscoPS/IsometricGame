using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float horizontalLimit = 8f;
    public float minHeight = 0.5f;
    public float maxHeight = 8f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.3f;
    private float nextFireTime;

    [Header("Health")]
    public int maxHealth = 3;
    private int currentHealth;
    private bool isDead = false;
    private bool isInvulnerable = false;
    
    [Header("Respawn Settings")]
    public float invulnerabilityDuration = 2f;
    public float respawnDelay = 1f;
    private Vector3 respawnPosition = new Vector3(0, 3, 0);

    private Rigidbody rb;
    private InputSystem_Actions inputActions;
    private Vector2 moveInput;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        inputActions.Gameplay.Enable();
        inputActions.Gameplay.Move.performed += OnMove;
        inputActions.Gameplay.Move.canceled += OnMove;
        inputActions.Gameplay.Fire.performed += OnFire;
    }

    void OnDisable()
    {
        inputActions.Gameplay.Move.performed -= OnMove;
        inputActions.Gameplay.Move.canceled -= OnMove;
        inputActions.Gameplay.Fire.performed -= OnFire;
        inputActions.Gameplay.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        currentHealth = maxHealth;

        if (firePoint == null)
        {
            GameObject fp = new GameObject("FirePoint");
            fp.transform.SetParent(transform);
            fp.transform.localPosition = new Vector3(0, 0, 1f);
            firePoint = fp.transform;
        }

        if (bulletPrefab == null)
        {
            bulletPrefab = CreatePlayerBulletPrefab();
        }
    }

    GameObject CreatePlayerBulletPrefab()
    {
        GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bullet.name = "PlayerBullet";
        bullet.transform.localScale = Vector3.one * 0.3f;

        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null)
            shader = Shader.Find("Standard");
        Material mat = new Material(shader);
        mat.color = Color.cyan;
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", Color.cyan * 0.5f);
        bullet.GetComponent<Renderer>().material = mat;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb == null)
            rb = bullet.AddComponent<Rigidbody>();
        rb.useGravity = false;

        SphereCollider col = bullet.GetComponent<SphereCollider>();
        col.isTrigger = true;

        bullet.tag = "PlayerBullet";

        Bullet bulletScript = bullet.AddComponent<Bullet>();
        bulletScript.speed = 25f;
        bulletScript.isPlayerBullet = true;

        bullet.SetActive(false);
        return bullet;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        Shoot();
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (isDead || isInvulnerable) return;
        
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0) * moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + movement;

        newPosition.x = Mathf.Clamp(newPosition.x, -horizontalLimit, horizontalLimit);
        newPosition.y = Mathf.Clamp(newPosition.y, minHeight, maxHeight);

        transform.position = newPosition;
    }

    void Shoot()
    {
        if (isDead) return;
        
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.SetActive(true);
            
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.isPlayerBullet = true;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead || isInvulnerable) return;
        
        currentHealth -= damage;
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateHealth(currentHealth);
        }
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            StartCoroutine(RespawnWithInvulnerability());
        }
    }

    System.Collections.IEnumerator RespawnWithInvulnerability()
    {
        isInvulnerable = true;
        
        CreatePlayerExplosion();
        
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
        {
            if (r != null) r.enabled = false;
        }
        
        yield return new UnityEngine.WaitForSeconds(respawnDelay);
        
        transform.position = respawnPosition;
        
        foreach (var r in renderers)
        {
            if (r != null) r.enabled = true;
        }
        
        float elapsed = 0f;
        while (elapsed < invulnerabilityDuration)
        {
            elapsed += Time.deltaTime;
            
            bool visible = (Mathf.FloorToInt(elapsed * 8f) % 2) == 0;
            foreach (var r in renderers)
            {
                if (r != null)
                {
                    r.enabled = visible;
                }
            }
            
            yield return null;
        }
        
        foreach (var r in renderers)
        {
            if (r != null)
            {
                r.enabled = true;
            }
        }
        
        isInvulnerable = false;
    }
    
    void CreatePlayerExplosion()
    {
        GameObject explosion = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        explosion.transform.position = transform.position;
        explosion.transform.localScale = Vector3.one * 0.5f;

        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null)
            shader = Shader.Find("Standard");
        Material mat = new Material(shader);
        mat.color = new Color(1f, 0.5f, 0f, 0.8f);
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", Color.red * 2f);
        explosion.GetComponent<Renderer>().material = mat;

        Destroy(explosion.GetComponent<Collider>());
        
        ExplosionEffect effect = explosion.AddComponent<ExplosionEffect>();
        Destroy(explosion, 0.5f);
    }

    void Die()
    {
        isDead = true;
        
        CreatePlayerExplosion();
        
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
        {
            if (r != null) r.enabled = false;
        }
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }
    
    public void ResetPlayer()
    {
        isDead = false;
        isInvulnerable = false;
        currentHealth = maxHealth;
        transform.position = respawnPosition;
        gameObject.SetActive(true);
        
        StopAllCoroutines();
        
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
        {
            if (r != null)
            {
                r.enabled = true;
                Color c = r.material.color;
                c.a = 1f;
                r.material.color = c;
            }
        }
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateHealth(currentHealth);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isDead || isInvulnerable) return;
        
        if (other.CompareTag("Obstacle"))
        {
            TakeDamage(1);
        }
        else if (other.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
        else if (other.CompareTag("EnemyBullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
