using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLogic : MonoBehaviour
{
    [SerializeField] public GameObject fireball;
    public GameObject winMenu;
    public Material blackFire;
    public Material blackSmoke;
    public GameObject player;
    public Transform fireballSpawnPoint;
    private Animator animator;
    public float fireballSpeed = 5f;
    public float spawnDistance = 1f;
    public int bossHealth = 250;
    public int maxHealth = 250;
    public bool phaseTwo = false;
    public bool phaseThree = false;
    private SpriteRenderer spriteRenderer;
    public float moveSpeed = 1.0f;
    void Start()
    {
        winMenu.SetActive(false);
        animator = GetComponent<Animator>();
        if(bossHealth == maxHealth && !phaseTwo && !phaseThree)
        {
            InvokeRepeating("Attack1Animation", 2.0f, 3.0f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerDirection = (Vector2)player.transform.position - (Vector2)transform.position;
        playerDirection.Normalize();

        if (phaseThree)
        {
            moveSpeed = 2f;
        }

        Vector2 newPosition = (Vector2)transform.position + playerDirection * moveSpeed * Time.deltaTime;
        transform.position = newPosition;
        if(bossHealth <= 0 )
        {
            bossHealth = 0;
            Destroy(gameObject);
            Time.timeScale = 0f;
            winMenu.SetActive(true);
        }

        if (bossHealth <= 125 && bossHealth >= 60 && !phaseTwo) 
        {
            phaseTwo = true;
            CancelInvoke("Attack1Animation");
            
            InvokeRepeating("Attack2Animation", 1.0f, 2.0f);
        }
        
        else if(bossHealth < 60 && !phaseThree)
        {
            phaseThree = true;
            animator.SetBool("Phase3", true);
            CancelInvoke("Attack2Animation");
            StartCoroutine(AttackSequence());

        }
    }

    public void Attack1Animation()
    {
        animator.SetTrigger("attack");
    }

    public void Attack2Animation()
    {
        animator.SetTrigger("attack02");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Energy")
        {
            Destroy(collision.collider.gameObject);
            bossHealth -= 50;
        }
    }

    public void AttackOne()
    {
        for (int i = 0; i < 8; i++)
        {
            float angle = i * 45f;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;

           
            Vector3 spawnPosition = (Vector2)fireballSpawnPoint.position + direction * spawnDistance;

            GameObject fireballClone = Instantiate(fireball, spawnPosition, Quaternion.identity);
            if (phaseThree)
            {
                fireballClone.transform.GetComponent<SpriteRenderer>().color = Color.blue;
                fireballSpeed = 7.0f;
            }
            Physics2D.IgnoreCollision(fireballClone.GetComponent<Collider2D>(), transform.GetComponent<Collider2D>());

            
            Vector2 velocity = direction * fireballSpeed;
            fireballClone.GetComponent<Rigidbody2D>().velocity = velocity;
        }
    }
    public void AttackTwo()
    {
        Vector2 playerDirection = (Vector2)player.transform.position - (Vector2)fireballSpawnPoint.position;
        playerDirection.Normalize();

        Vector3 spawnPosition = (Vector2)fireballSpawnPoint.position;

        GameObject fireballClone = Instantiate(fireball, spawnPosition, Quaternion.identity);
        if (phaseThree)
        {
            fireballClone.transform.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        fireballClone.tag = "BigFireBall";
        fireballClone.transform.localScale = new Vector3(fireballClone.transform.localScale.x*2, fireballClone.transform.localScale.y * 2, 1.0f);
        Physics2D.IgnoreCollision(fireballClone.GetComponent<Collider2D>(), transform.GetComponent<Collider2D>());
    }

    private IEnumerator AttackSequence()
    {
        ChangeColor();
        yield return new WaitForSeconds(2.0f);

        while (true)
        {
            Attack1Animation();
            yield return new WaitForSeconds(2.0f);
            Attack2Animation();
            yield return new WaitForSeconds(2.0f);
        }
    }


    public void ChangeColor()
    { 
        Transform r_hand = transform.GetChild(0).GetChild(5).GetChild(0);
        Transform vfx = transform.GetChild(2);
        Transform fire = r_hand.transform.GetChild(1);

        spriteRenderer = r_hand.GetComponent<SpriteRenderer>();
        spriteRenderer.material.color = new Color(0x00 / 255f, 0x01 / 255f, 0x9A / 255f);


        fire.GetComponent<ParticleSystemRenderer>().material = blackFire;
        vfx.GetComponent<ParticleSystemRenderer>().material = blackSmoke;
    }
}