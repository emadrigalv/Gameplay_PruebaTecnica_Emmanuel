using System.Collections;
using UnityEngine;

public class BlockColorHandler: MonoBehaviour
{
    [Header("Dependecies")]
    [SerializeField] private Renderer blockRenderer;

    [Header("Parameters")]
    [SerializeField] private float colorTransitionDuration = 0.5f;


    public void InitializeBlockColor(int blockHealthPoints)
    {
        blockRenderer.material.color = GetNewColor(blockHealthPoints);
    }

    public void ChangeColor(int blockHealthPoints)
    {
        Color endColor = GetNewColor(blockHealthPoints);
        StartCoroutine(ColorLerpCoroutine(endColor));
    }

    private Color GetNewColor(int blockHealthPoints)
    {
        switch (blockHealthPoints)
        {
            case 1:
                return new Color(0.8f, 0.9f, 0.8f); // Difficulty Level 1, 1 hit to destroy
            case 2:
                return new Color(0.9f, 0.8f, 0.6f); // Difficulty Level 2, 2 hit to destroy
            case 3:
                return new Color(0.9f, 0.7f, 0.8f); // Difficulty Level 3, 3 hit to destroy
            case 4:
                return new Color(0.7f, 0.8f, 0.9f); // Difficulty Level 4, 4 hit to destroy
            case 5:
                return new Color(0.9f, 0.9f, 0.6f); // Difficulty Level 5, 5 hit to destroy
            default:
                return Color.white; //block health points out of range, Error!
        }
    }

    private IEnumerator ColorLerpCoroutine(Color endColor)
    {
        float elapsedTime = 0f;
        Color currentColor = blockRenderer.material.color;

        while (elapsedTime < colorTransitionDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / colorTransitionDuration; //Interpolation value

            blockRenderer.material.color = Color.Lerp(currentColor, endColor, t);

            yield return null;
        }

        blockRenderer.material.color = endColor;
    }
}