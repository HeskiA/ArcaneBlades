using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private GameObject player;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    { 
        Vector2 direction = player.transform.position - transform.forward;
        direction.Normalize();
        if(gameObject.tag == "BigFireBall")
        {
           
           
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(gameObject);
    }
}
