using UnityEngine;

public class Destructible : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Score")]
    public int scoreValue = 10;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scoreValue);
        }

        CreateSimpleExplosion();
        Destroy(gameObject);
    }

    void CreateSimpleExplosion()
    {
        GameObject explosion = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        explosion.transform.position = transform.position;
        explosion.transform.localScale = Vector3.one * 0.5f;

        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null)
            shader = Shader.Find("Standard");
        Material mat = new Material(shader);
        mat.color = new Color(1f, 0.8f, 0f, 0.8f);
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", Color.yellow * 2f);
        explosion.GetComponent<Renderer>().material = mat;

        Destroy(explosion.GetComponent<Collider>());
        
        ExplosionEffect effect = explosion.AddComponent<ExplosionEffect>();
        Destroy(explosion, 0.5f);
    }
}

public class ExplosionEffect : MonoBehaviour
{
    private float expansionSpeed = 8f;
    private float maxSize = 3f;
    
    void Update()
    {
        transform.localScale += Vector3.one * expansionSpeed * Time.deltaTime;
        
        if (transform.localScale.x >= maxSize)
        {
            Destroy(gameObject);
        }
        
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            Color col = rend.material.color;
            col.a -= Time.deltaTime * 2f;
            rend.material.color = col;
        }
    }
}
