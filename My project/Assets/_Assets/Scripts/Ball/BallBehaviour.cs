using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Rigidbody rb;

    private float speed = 10f;

    void Start()
    {
        Vector3 initialDirection = new Vector3(1, 0, 0).normalized; 
        rb.velocity = initialDirection * speed; 
    }

}
