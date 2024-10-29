using System.Collections;
using UnityEngine;

public class SpeedPowerUp : PowerUpBase
{
    [Header("Dependencies")]
    [SerializeField] private BallBehaviour ballBehaviour;
    [SerializeField] private Rigidbody ballRb;

    [Header("Parameters")]
    [SerializeField] private float powerUpSpeed = 20f;
    [SerializeField] private float powerUpTime = 5f;

    private float normalSpeed;
    private bool isActive;

    private Coroutine coroutine;

    private void Awake()
    {
        normalSpeed = ballBehaviour.Speed;
    }

    [ContextMenu("Speed Down Power Up")]
    public override void StartPowerUp()
    {
        if (isActive || !gameObject.activeSelf) return;
        else
        {
            isActive = true;
            coroutine = StartCoroutine(SpeedUpCoroutine());
        }
    }

    public override void StopPowerUp()
    {
        if (!isActive) return;
        StopCoroutine(coroutine);
        SetVelocity(normalSpeed);
        isActive = false;
    }

    private IEnumerator SpeedUpCoroutine()
    {
        SetVelocity(powerUpSpeed);
        yield return new WaitForSeconds(powerUpTime);
        SetVelocity(normalSpeed);
        isActive = false;
    }

    private void SetVelocity(float newSpeed)
    {
        Vector3 currentDirection = ballRb.velocity.normalized;

        ballRb.velocity = currentDirection * newSpeed;
    }
}
