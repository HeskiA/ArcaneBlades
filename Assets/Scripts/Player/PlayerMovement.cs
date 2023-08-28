//using System.Collections;
//using System.Collections.Generic;
using System.Collections;
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
    public LevelManager levelManager;
    private float horizontalAxis;
    private float verticalAxis;
    Vector2 mousePosition;
    public GameObject death;
    public GameObject nextLevelPanel;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody2D>();
        if(death)
            death.SetActive(false);

        if(nextLevelPanel)
            nextLevelPanel.SetActive(false);

        speed = 10;
        health = 200;
        if (generator != null)
        {
            generator.clearMap();
            generator.CorridorFirstGeneration();
        }
        
        healthCounter.text = "Health: " + health;
        
    }

    private void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        healthCounter.text = "Health: " + health;

        if (horizontalAxis == 0)
        {
            if (mousePosition.x < transform.position.x)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            if (horizontalAxis < 0) transform.localRotation = Quaternion.Euler(0, 180, 0);
            else if (horizontalAxis > 0) transform.localRotation = Quaternion.Euler(0, 0, 0);
        }


        animator.SetBool("running", horizontalAxis != 0 || verticalAxis != 0);
        if(health<=0 )
        {
            Time.timeScale = 0f;
            if(levelManager)
                levelManager.OnDeath();
            death.SetActive(true);
        }
    }


    void FixedUpdate()
    {
        body.velocity = new Vector2(horizontalAxis * speed, verticalAxis * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            damagePlayer(levelManager.GetDamage());
            audioSource.Play();
            audioSource.loop = true;
        }
            

        else if(collision.collider.tag == "Fireball")
        {
            audioSource.Play();
            damagePlayer(10);
        }
        else if (collision.collider.tag == "BigFireBall")
        {
            audioSource.Play();
            damagePlayer(80);
        }
        else if(collision.collider.tag == "NextLevel")
        {
            StartCoroutine(goToNextLevel(3f));
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
            damagePlayer(1);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            audioSource.loop = false;
            audioSource.Stop();
        }
            
    }

    public void damagePlayer(int damage)
    {
        health -= damage;
        if(health <= 0 )
            health = 0;
    }

    public int getHealth()
    {
        return health;
    }

    IEnumerator goToNextLevel(float delay)
    {
        nextLevelPanel.SetActive(true);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(delay);
        nextLevelPanel.SetActive(false);
        levelManager.IncrementLevel();
        Time.timeScale = 1f;
        generator.clearMap();
        generator.CorridorFirstGeneration();
        if (health < 100)
            health = 100;
        transform.position = new Vector3(0, 0, 0);
    }
}
