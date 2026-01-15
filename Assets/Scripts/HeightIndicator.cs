using UnityEngine;
using UnityEngine.UI;

public class HeightIndicator : MonoBehaviour
{
    public Transform player;
    public Slider heightSlider;
    public Text heightText;
    public float minHeight = 0f;
    public float maxHeight = 10f;

    void Update()
    {
        if (player != null)
        {
            float currentHeight = player.position.y;
            float normalizedHeight = Mathf.InverseLerp(minHeight, maxHeight, currentHeight);

            if (heightSlider != null)
            {
                heightSlider.value = normalizedHeight;
            }

            if (heightText != null)
            {
                heightText.text = $"Altura: {currentHeight:F1}m";
            }
        }
    }
}
