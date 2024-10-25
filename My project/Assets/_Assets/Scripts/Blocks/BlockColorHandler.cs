using System.Collections;
using UnityEngine;

public class BlockColorHandler: MonoBehaviour
{
    [Header("Dependecies")]
    [SerializeField] private Renderer blockRenderer;

    [Header("Parameters")]
    [SerializeField] private float transitionDuration = 0.3f;


    public void InitializeBlockColor(int blockLevel)
    {
        blockRenderer.material.color = GetNewColor(blockLevel);
    }

    // Transition to a color with emission and go back to the current color, impact effect
    public void BlockHitAnimation()
    {
        StartCoroutine(EmissionTransitionCoroutine());
    }

    private Color GetNewColor(int blockLevel)
    {
        switch (blockLevel)
        {
            case 1: // Green
                return new Color(0.6f, 1f, 0.6f); // Difficulty Level 1, 1 hit to destroy
            case 2: // Yellow
                return new Color(1f, 1f, 0.6f); // Difficulty Level 2, 3 hit to destroy
            case 3: // Pink
                return new Color(1f, 0.8f, 0.86f); // Difficulty Level 3, 5 hit to destroy
            default:
                return Color.white; //block health points out of range, Error!
        }
    }

    private IEnumerator EmissionTransitionCoroutine()
    {

        blockRenderer.material.EnableKeyword("_EMISSION");

        Color initialEmissionColor = Color.black;
        Color targetEmissionColor = blockRenderer.material.color;

        yield return StartCoroutine(EmissionColorTransition(transitionDuration, initialEmissionColor, targetEmissionColor));

        blockRenderer.material.SetColor("_EmissionColor", targetEmissionColor);

        yield return StartCoroutine(EmissionColorTransition(transitionDuration, targetEmissionColor, initialEmissionColor));

        blockRenderer.material.DisableKeyword("_EMISSION");
    }

    /// <summary>
    /// Transitions the emission from color a to color b in the duration provided
    /// </summary>
    /// <param name="duration"></param>The time in seconds it takes to complete the transition.
    /// <param name="a"></param>The initial color of the emission
    /// <param name="b"></param>The target emission color
    private IEnumerator EmissionColorTransition(float duration, Color a, Color b)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {

            float t = elapsedTime / duration;

            Color currentEmissionColor = Color.Lerp(a, b, t);
            blockRenderer.material.SetColor("_EmissionColor", currentEmissionColor);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}