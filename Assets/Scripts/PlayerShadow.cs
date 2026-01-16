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
        shadowObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
        shadowObject.name = "PlayerShadow";

        Destroy(shadowObject.GetComponent<Collider>());

        shadowObject.transform.rotation = Quaternion.Euler(90, 0, 0);

        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null)
            shader = Shader.Find("Standard");
        Material shadowMat = new Material(shader);
        shadowMat.color = new Color(0, 0, 0, 0.5f);
        shadowMat.SetFloat("_Mode", 3);
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
            Vector3 shadowPos = player.position;
            shadowPos.y = groundLevel + 0.01f;
            shadowObject.transform.position = shadowPos;

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
