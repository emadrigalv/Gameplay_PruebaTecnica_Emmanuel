using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class BallBehaviour : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PadelController paddle;
    [SerializeField] private Rigidbody rb;

    [Header("Parameters")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float powerUpSpeed = 20f;
    [SerializeField] private float speedUpTime = 5f;

    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        AdjustBallAngle();
    }

    // Ball avoid horizontal and vertical movements
    private void AdjustBallAngle()
    {
        Vector3 velocity = rb.velocity;

        if (Mathf.Abs(velocity.x) < 0.2f)
        {
            velocity.x = Mathf.Sign(velocity.x) * 0.7f;
        }

        if (Mathf.Abs(velocity.y) < 0.2f)
        {
            velocity.y = Mathf.Sign(velocity.y) * 0.7f;
        }

        rb.velocity = velocity;
    }

    [ContextMenu("Speed Up")]
    public void SpeedUp()
    {
        StartCoroutine(SpeedUpCoroutine());
    }

    private IEnumerator SpeedUpCoroutine()
    {
        SetVelocity(powerUpSpeed);
        yield return new WaitForSeconds(speedUpTime);
        SetVelocity(speed);
    }

    private void SetVelocity(float newSpeed)
    {
        Vector3 currentDirection = rb.velocity.normalized;

        rb.velocity = currentDirection * newSpeed;
    }

    [ContextMenu("Initialize ball")]
    public void StickBallToPaddle()
    {
        rb.velocity = Vector3.zero;

        transform.SetParent(paddle.transform);

        transform.localPosition = new(0, 1.5f, 0);
    }

    [ContextMenu("Launch ball")]
    public void LaunchBall()
    {
        transform.SetParent(null);

        Vector3 initialDirection = new Vector3(0.5f, 1, 0).normalized;

        rb.velocity = initialDirection * speed;
    }
}
