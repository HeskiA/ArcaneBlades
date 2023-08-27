using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private GameObject player;
    public float speed;
    Animator animator;
    private GameObject levelManager;
    private LevelManager levelManagerObj;

    public float distanceBetween;
    private float distance;

    private void Awake()
    {

    }
    void Start()
    {
        levelManager = GameObject.Find("LevelManager");
        levelManagerObj = levelManager.GetComponent<LevelManager>();
        speed = levelManagerObj.GetSpeed();
        animator = GetComponent<Animator>();
        
        player = GameObject.Find("Player");
    }


    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.forward;
        direction.Normalize();


        if(distance < distanceBetween && distance > 0.5) 
        {
            animator.SetBool("SeePlayer", true);
            
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

            if (player.transform.position.x - this.transform.position.x > 0f)
            {
                
                this.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                this.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

        }
        else
        {
            animator.SetBool("SeePlayer", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Energy")
        {
            LevelManager.incrementScore();
            LevelManager.enemyDeath();
            Destroy(collision.collider.gameObject);
            Destroy(gameObject);
        }
    }


}
