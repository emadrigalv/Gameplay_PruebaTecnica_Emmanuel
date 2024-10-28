using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PadelController paddle;
    [SerializeField] private Rigidbody rb;

    [Header("Parameters")]
    [SerializeField] private float speed = 15f;
    [SerializeField] private float minAxisVelocity = 4;
    [SerializeField] private float newAxisVelocity = 7;
    public float Speed { get { return speed; } private set { } }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fall")) paddle.PlayerDeath();

        else
        {
            AdjustBallAngle();
            //Sound
        }
    }

    // Ball avoid horizontal and vertical movements
    private void AdjustBallAngle()
    {
        Vector3 velocity = rb.velocity;

        // Verify horizontal velocity is not too low
        if (Mathf.Abs(velocity.x) < minAxisVelocity)
        {
            velocity.x = Mathf.Sign(velocity.x) * newAxisVelocity; 
        }

        //Verify vertical velocity is not too low
        if (Mathf.Abs(velocity.y) < minAxisVelocity)
        {
            velocity.y = Mathf.Sign(velocity.y) * newAxisVelocity;
        }

        rb.velocity = (velocity.magnitude > speed) ? velocity.normalized * speed : velocity;
    }

    [ContextMenu("Initialize ball")]
    public void StickBallToPaddle()
    {
        transform.SetParent(paddle.transform);

        transform.localPosition = new(0, 1.1f, 0);

        rb.velocity = Vector3.zero;

        paddle.ballAttached = true;
    }

    [ContextMenu("Launch ball")]
    public void LaunchBall()
    {
        transform.SetParent(null);

        Vector3 initialDirection = new Vector3(0.5f, 1, 0).normalized;

        rb.velocity = initialDirection * speed;
    }
}
