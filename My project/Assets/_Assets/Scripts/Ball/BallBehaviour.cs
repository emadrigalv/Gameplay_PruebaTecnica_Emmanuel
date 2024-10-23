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
        Vector3 initialDirection = new Vector3(0.3f, 1, 0).normalized; 

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


}
