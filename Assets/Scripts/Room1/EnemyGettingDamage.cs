using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGettingDamage : MonoBehaviour
{
    Animator animator;
    public int maxHealth = 100;
    public int currentHealth;

    private bool canTakeDamage = true;
    private float cooldownTimer = 0f;
    public float cooldownDuration = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (!canTakeDamage)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                canTakeDamage = true;
            }
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 directionToEnemy = transform.position - player.transform.position;
            directionToEnemy.y = 0f; // Ignore vertical difference if not needed

            if (Vector3.Dot(player.transform.forward, directionToEnemy.normalized) > 0.5f && Vector3.Distance(transform.position, player.transform.position) <= 2.0f && Input.GetKey(KeyCode.Mouse0) && canTakeDamage)
            {
                currentHealth -= 50;
                canTakeDamage = false;
                cooldownTimer = cooldownDuration;
            }
        }
    }
}
