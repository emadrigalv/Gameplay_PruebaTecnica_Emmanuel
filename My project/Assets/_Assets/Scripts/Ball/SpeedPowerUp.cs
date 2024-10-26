using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        normalSpeed = ballBehaviour.Speed;
    }

    [ContextMenu("Speed Down Power Up")]
    public override void StartPowerUp()
    {
        StartCoroutine(SpeedUpCoroutine());
    }

    private IEnumerator SpeedUpCoroutine()
    {
        SetVelocity(powerUpSpeed);
        yield return new WaitForSeconds(powerUpTime);
        SetVelocity(normalSpeed);
    }

    private void SetVelocity(float newSpeed)
    {
        Vector3 currentDirection = ballRb.velocity.normalized;

        ballRb.velocity = currentDirection * newSpeed;
    }
}
