using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PadelController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private BallBehaviour ball;

    [Header("Parameters")]
    [SerializeField] private string deathVfxTag;
    [SerializeField] private string deathSfxTag;
    [SerializeField] private float deathAnimTime;
    [SerializeField] private float padelSpeed;
    [SerializeField] private float minLimitX;
    [SerializeField] private float maxLimitX;
    [SerializeField] private float shakeIntensity = 5;
    [SerializeField] private float shakeDuration = 0.5f;

    private PlayerControls playerControls;

    public bool ballAttached;
    public bool isAlive = true;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        ball.StickBallToPaddle();
        ballAttached = true;
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Player.Fire.started += Fire;
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
            HandleMove();
    }

    private void HandleMove()
    {
        if (!isAlive) return;

        Vector2 input = playerControls.Player.Move.ReadValue<Vector2>();

        Vector3 movePadel = new Vector3(input.x, 0, 0) * padelSpeed * Time.deltaTime;

        transform.Translate(movePadel);

        //Restrict paddle movement on the x-axis between the minimum and maximum limits.
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minLimitX + transform.localScale.x/2, maxLimitX - transform.localScale.x / 2);

        transform.position = clampedPosition;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        if (!isAlive || !ballAttached) return;

        ball.LaunchBall();
        ballAttached = false;
    }

    public void PlayerDeath()
    {
        if(!isAlive) return;

        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        isAlive = false;
        ball.gameObject.SetActive(false);

        Pooler.instance.SpawnFromPool(deathVfxTag, transform.position - Vector3.forward);
        AudioManager.instance.Play(deathSfxTag);
        CameraShake.instance.ShakeCamera(shakeIntensity, shakeDuration);

        yield return new WaitForSeconds(deathAnimTime);

        ball.gameObject.SetActive(true);
        ball.StickBallToPaddle();

        isAlive = false; // to verify
    }

}
