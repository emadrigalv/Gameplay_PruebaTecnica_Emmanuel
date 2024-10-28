using System.Collections;
using UnityEngine;

public class ScalePowerUp : PowerUpBase
{
    [Header("Parameters")]
    [SerializeField] private float scaleUpX = 6f;
    [SerializeField] private float powerUpTime = 15f;
    [SerializeField] private float scaleTransitionTime = 0.5f;

    private bool isActive = false;

    [ContextMenu("Scale Up")]
    public override void StartPowerUp()
    {
        if (isActive) return;
        else StartCoroutine(ScaleUpCoroutine());
    }

    private IEnumerator ScaleUpCoroutine()
    {
        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = new(scaleUpX, 1, 1);

        yield return StartCoroutine(ScaleTransition(scaleTransitionTime, initialScale, targetScale));

        yield return new WaitForSeconds(powerUpTime);

        yield return StartCoroutine(ScaleTransition(scaleTransitionTime, targetScale, initialScale));
    }

    /// <summary>
    /// Transition the object scale from startScale to targetScale
    /// </summary>
    /// <param name="duration"></param>Transition duration
    /// <param name="startScale"></param>Initial scale
    /// <param name="targetScale"></param>Final scale
    /// <returns></returns>
    private IEnumerator ScaleTransition(float duration, Vector3 startScale, Vector3 targetScale)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {

            float t = elapsedTime / duration;

            transform.localScale = Vector3.Lerp(startScale, targetScale, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localScale = targetScale;
    }
}
