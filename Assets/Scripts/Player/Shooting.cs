using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public GameObject energy;
    public Transform firePoint;
    public float energySpeed = 20;
    public float maxDistance = 4f;
    public float shootingCooldown = 0.5f;
    public GameObject player;
    public float timeBetweenShoot;
    private float timer;

    Vector2 lookDirection;
    float lookAngle;
    bool canShoot = true;

    public void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);
        if (!canShoot)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenShoot)
            {
                canShoot = true;
                animator.ResetTrigger("shoot");
                timer = 0;
            }
        }

        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(ShootWithDelay());
        }
    }

    IEnumerator ShootWithDelay()
    {
        animator.SetTrigger("shoot");
        canShoot = false;
        yield return new WaitForSeconds(0.3f);
        GameObject energyClone = Instantiate(energy);
        energyClone.transform.position = firePoint.position;
        energyClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

        Physics2D.IgnoreCollision(energyClone.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
        energyClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * energySpeed;
        StartCoroutine(DestroyEnergyAfterDistance(energyClone, maxDistance));
    }

    IEnumerator DestroyEnergyAfterDistance(GameObject energyClone, float distance)
    {
        float initialDistance = Vector2.Distance(energyClone.transform.position, firePoint.position);

        while (Vector2.Distance(energyClone.transform.position, firePoint.position) - initialDistance <= distance)
        {
            yield return null;
        }

        Destroy(energyClone);
    }
}
