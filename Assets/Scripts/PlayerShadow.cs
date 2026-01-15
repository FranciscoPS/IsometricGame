using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public Transform player;
    public float groundLevel = 0f;
    public float shadowScale = 0.8f;

    private GameObject shadowObject;

    void Start()
    {
        if (player == null)
        {
            player = transform;
        }

        CreateShadow();
    }

    void CreateShadow()
    {
        // Crear quad para la sombra
        shadowObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
        shadowObject.name = "PlayerShadow";
        
        // Eliminar collider
        Destroy(shadowObject.GetComponent<Collider>());
        
        // Rotar para que esté en el suelo
        shadowObject.transform.rotation = Quaternion.Euler(90, 0, 0);
        
        // Crear material semi-transparente
        Material shadowMat = new Material(Shader.Find("Standard"));
        shadowMat.color = new Color(0, 0, 0, 0.5f);
        shadowMat.SetFloat("_Mode", 3); // Transparent
        shadowMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        shadowMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        shadowMat.SetInt("_ZWrite", 0);
        shadowMat.DisableKeyword("_ALPHATEST_ON");
        shadowMat.EnableKeyword("_ALPHABLEND_ON");
        shadowMat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        shadowMat.renderQueue = 3000;
        
        shadowObject.GetComponent<Renderer>().material = shadowMat;
        shadowObject.transform.localScale = Vector3.one * shadowScale;
    }

    void Update()
    {
        if (shadowObject != null && player != null)
        {
            // Posicionar sombra en el suelo debajo del jugador
            Vector3 shadowPos = player.position;
            shadowPos.y = groundLevel + 0.01f; // Ligeramente sobre el suelo para evitar z-fighting
            shadowObject.transform.position = shadowPos;

            // Escalar según altura del jugador
            float heightFactor = 1f - (player.position.y / 10f);
            heightFactor = Mathf.Clamp(heightFactor, 0.3f, 1f);
            shadowObject.transform.localScale = Vector3.one * shadowScale * heightFactor;
        }
    }

    void OnDestroy()
    {
        if (shadowObject != null)
        {
            Destroy(shadowObject);
        }
    }
}
