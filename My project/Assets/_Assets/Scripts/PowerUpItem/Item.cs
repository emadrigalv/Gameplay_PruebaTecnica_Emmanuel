using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Rigidbody rb;

    [Header("Parameters")]
    public int ID;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private float desappearDuration = 0.5f;
    [SerializeField, Range(0, 1)] private float shrinkFactor = 0.3f;

    private bool inTransition = false;

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    void OnEnable()
    {
        transform.localScale = originalScale;
        rb.isKinematic = false;
    }

    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fall"))
        {
            ItemPicked();
        }
    }

    public void ItemPicked()
    {
        if (inTransition) return;
        else
        {
            rb.isKinematic = true;
            Vector3 initialScale = transform.localScale;
            Vector3 finalScale = transform.localScale * shrinkFactor;
            StartCoroutine(DesappearingCoroutine(desappearDuration, initialScale, finalScale));
        }
    }

    /// <summary>
    /// Transitions de original scale to a small scale to reproduce a desappearing effect
    /// </summary>
    /// <param name="duration"></param>Effect duration
    /// <param name="startScale"></param>Original scale
    /// <param name="finalScale"></param>Final scale before desappear the object
    /// <returns></returns>
    private IEnumerator DesappearingCoroutine(float duration, Vector3 startScale, Vector3 finalScale)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {

            float t = elapsedTime / duration;

            transform.localScale = Vector3.Lerp(startScale, finalScale, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localScale = finalScale;

        this.gameObject.SetActive(false);
    }
}
