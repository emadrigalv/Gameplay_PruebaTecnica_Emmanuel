using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PadelController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private BallBehaviour ball;

    [Header("Parameters")]
    [SerializeField] private float padelSpeed = 7f;
    [SerializeField] private float minLimitX = 20f;
    [SerializeField] private float maxLimitX = 40f;

    private PlayerControls playerControls;

    public bool ballAttached;

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
        Vector2 input = playerControls.Player.Move.ReadValue<Vector2>();

        Vector3 movePadel = new Vector3(input.x, 0, 0) * padelSpeed * Time.deltaTime;

        transform.Translate(movePadel);

        //Restrict paddle movement on the x-axis between the minimum and maximum limits.
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minLimitX, maxLimitX);

        transform.position = clampedPosition;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        ball.LaunchBall();
        ballAttached = false;
    }



}
