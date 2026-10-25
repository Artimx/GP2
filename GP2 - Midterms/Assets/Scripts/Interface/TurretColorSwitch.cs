
using UnityEngine;

public class TurretColorSwitch : MonoBehaviour, ISwitchable
{
    [Header("Color Settings")]
    [SerializeField] private Renderer colorIndicator;
    [SerializeField] private Material[] colorMaterials;

    private int currentColorIndex = 0;

    public Material CurrentMaterial
    {
        get { return colorMaterials[currentColorIndex]; }
    }

    private void Start()
    {
        ApplyColor();
    }

    public void Switch()
    {
        currentColorIndex++;

        if (currentColorIndex >= colorMaterials.Length)
        {
            currentColorIndex = 0;
        }

        ApplyColor();
    }

    private void ApplyColor()
    {
        if (colorIndicator == null || colorMaterials.Length == 0)
        {
            Debug.LogWarning("Assign the Color Indicator and color materials in the Inspector.");
            return;
        }

        colorIndicator.material = colorMaterials[currentColorIndex];
    }
}

