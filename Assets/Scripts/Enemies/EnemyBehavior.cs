using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private GameObject player;
    //private PlayerMovement playerMovement;
    public float speed;

    public float distanceBetween;
    private float distance;
    void Start()
    {
        
        player = GameObject.Find("Player");
        //playerMovement = player.GetComponent<PlayerMovement>();
    }


    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.forward;
        direction.Normalize();

        //float angle = Mathf.Atan2(direction.y, direction.x)* Mathf.Rad2Deg;

        
        if(distance < distanceBetween && distance > 1) 
        {

            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(Vector3.forward*angle);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Energy")
        {
            Destroy(collision.collider.gameObject);
            Destroy(gameObject);
        }
    }


}
