using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Rigidbody rb;

    [Header("Parameters")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float powerUpSpeed = 20f;
    [SerializeField] private float speedUpTime = 5f;

    void Start()
    {
        Vector3 initialDirection = new Vector3(0.5f, 1, 0).normalized; 

        rb.velocity = initialDirection * speed; 
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

}
