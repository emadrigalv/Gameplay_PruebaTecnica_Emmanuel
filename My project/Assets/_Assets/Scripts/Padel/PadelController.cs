using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PadelController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float padelSpeed = 7f;
    [SerializeField] private float minLimitX = 20f;
    [SerializeField] private float maxLimitX = 40f;

    private PlayerControls playerControls;

    private InputAction move;
    private InputAction fire;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    private void Update()
    {
        Vector2 input = move.ReadValue<Vector2>();

        Vector3 movePadel = new Vector3(input.x, 0, 0) * padelSpeed * Time.deltaTime;

        transform.Translate(movePadel);

        //Restrict paddle movement on the x-axis between the minimum and maximum limits.
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minLimitX, maxLimitX);

        transform.position = clampedPosition;
    }


}
