//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    private float horizontalAxis;
    private float verticalAxis;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        speed = 10;
    }

    private void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");  // GetAxisRaw
        verticalAxis = Input.GetAxis("Vertical");  // GetAxisRaw
    }


    void FixedUpdate()
    {
        // left/right movement
        body.velocity = new Vector2(horizontalAxis * speed, verticalAxis * speed);
        //body.AddForce(transform.right * horizontalAxis * speed);

    }
}
