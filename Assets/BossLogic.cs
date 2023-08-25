using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLogic : MonoBehaviour
{
    [SerializeField] public GameObject fireball;
    public GameObject player;
    public Transform fireballSpawnPoint;
    private Animator animator;
    public float fireballSpeed = 5f;
    public float spawnDistance = 1f;
    public int bossHealth = 250;
    public int maxHealth = 250;
    public bool phaseTwo = false;
    public bool phaseThree = false;
    private bool inDestructible = false;
    private Coroutine attackSequenceCoroutine;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        animator = GetComponent<Animator>();
        if(bossHealth == maxHealth && !phaseTwo && !phaseThree)
        {
            InvokeRepeating("Attack1Animation", 5.0f, 5.0f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHealth <= 125 && bossHealth >= 60 && !phaseTwo) 
        {
            phaseTwo = true;
            CancelInvoke("Attack1Animation");
            
            InvokeRepeating("Attack2Animation", 2.0f, 2.0f);
        }
        
        else if(bossHealth < 60 && !phaseThree)
        {
            phaseThree = true;
            animator.SetBool("Phase3", true);
            CancelInvoke("Attack2Animation");
            attackSequenceCoroutine = StartCoroutine(AttackSequence());

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


    public void AttackOne()
    {
        for (int i = 0; i < 8; i++)
        {
            float angle = i * 45f;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;

           
            Vector3 spawnPosition = (Vector2)fireballSpawnPoint.position + direction * spawnDistance;

            GameObject fireballClone = Instantiate(fireball, spawnPosition, Quaternion.identity);
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
        fireballClone.tag = "BigFireBall";
        fireballClone.transform.localScale = new Vector3(fireballClone.transform.localScale.x*2, fireballClone.transform.localScale.y * 2, 1.0f);
        Physics2D.IgnoreCollision(fireballClone.GetComponent<Collider2D>(), transform.GetComponent<Collider2D>());
    }

    private IEnumerator AttackSequence()
    {
        inDestructible = true;
        ChangeColor();
        yield return new WaitForSeconds(2.0f);
        inDestructible = false;
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

        Transform body = transform.GetChild(0);

        Transform eye = body.GetChild(0);
        Transform eye_Shiny = body.GetChild(1);
        Transform r_leg = body.GetChild(3);
        Transform l_leg = body.GetChild(4);
        Transform r_arm = body.GetChild(5);
        Transform l_arm = body.GetChild(6);

        Transform r_hand = r_arm.GetChild(0);
        Transform l_hand = l_arm.GetChild(0);

        spriteRenderer = body.GetComponent<SpriteRenderer>();
        spriteRenderer.material.color = new Color(0x00 / 255f, 0x18 / 255f, 0xFF / 255f);

        spriteRenderer = r_leg.GetComponent<SpriteRenderer>();
        spriteRenderer.material.color = new Color(0x00 / 255f, 0x18 / 255f, 0xFF / 255f);
        spriteRenderer = l_leg.GetComponent<SpriteRenderer>();
        spriteRenderer.material.color = new Color(0x00 / 255f, 0x18 / 255f, 0xFF / 255f);
        spriteRenderer = r_arm.GetComponent<SpriteRenderer>();
        spriteRenderer.material.color = new Color(0x00 / 255f, 0x18 / 255f, 0xFF / 255f);
        spriteRenderer = l_arm.GetComponent<SpriteRenderer>();
        spriteRenderer.material.color = new Color(0x00 / 255f, 0x18 / 255f, 0xFF / 255f);
        spriteRenderer = eye.GetComponent<SpriteRenderer>();
        spriteRenderer.material.color = new Color(0x00 / 255f, 0xDD / 255f, 0xFD / 255f);


        spriteRenderer = r_hand.GetComponent<SpriteRenderer>();
        spriteRenderer.material.color = new Color(0x00 / 255f, 0x83 / 255f, 0xFF / 255f);

        spriteRenderer = l_hand.GetComponent<SpriteRenderer>();
        spriteRenderer.material.color = new Color(0x00 / 255f, 0x83 / 255f, 0xFF / 255f);

    }
}



