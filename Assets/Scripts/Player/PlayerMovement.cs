//using System.Collections;
//using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private CorridorFirstDugneonGen generator;
    [SerializeField] private int health;
    [SerializeField] private TMP_Text healthCounter;
    [SerializeField] private Animator animator;
    private float horizontalAxis;
    private float verticalAxis;
    private bool escPressed;
    Vector2 mousePosition;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        speed = 10;
        health = 200;
        generator.clearMap();
        generator.CorridorFirstGeneration();
        healthCounter.text = "Health: " + health;
        
    }

    private void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");  // GetAxisRaw
        verticalAxis = Input.GetAxis("Vertical");  // GetAxisRaw

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x < transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        animator.SetBool("running", horizontalAxis != 0 || verticalAxis != 0);
        escPressed = Input.GetKey(KeyCode.Escape);
        if(health<=0 )
        {
            SceneManager.LoadScene(0);
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
            damagePlayer(10);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
            damagePlayer(1);
    }

    public void damagePlayer(int damage)
    {
        health -= damage;
        healthCounter.text = "Health: " + health;
    }

    public int getHealth()
    {
        return health;
    }


}