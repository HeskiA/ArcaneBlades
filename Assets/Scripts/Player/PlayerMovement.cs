//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private CorridorFirstDugneonGen generator;
    private float horizontalAxis;
    private float verticalAxis;
    private bool escPressed;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        speed = 10;
        generator.clearMap();
        generator.CorridorFirstGeneration();
        
    }

    private void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");  // GetAxisRaw
        verticalAxis = Input.GetAxis("Vertical");  // GetAxisRaw
        escPressed = Input.GetKey(KeyCode.Escape);
    }


    void FixedUpdate()
    {
        body.velocity = new Vector2(horizontalAxis * speed, verticalAxis * speed);
        if (escPressed)
        {
            SceneManager.LoadScene(0);
        }
        //body.AddForce(transform.right * horizontalAxis * speed);

    }
}
