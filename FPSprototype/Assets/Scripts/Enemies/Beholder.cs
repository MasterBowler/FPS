using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beholder : MonoBehaviour
{
    public Transform player;
    public float health = 50f;
    public float speed = 10f;
    public float sightRange = 20f;
    public float attackRange = 2f;

    bool chasePlayer = false;

    Animator animator;
    public PlayerHealth playerHealth;

    GameManager gameManager;

    void Start()
    {
        player = GameObject.Find("First Person Player").transform;
        playerHealth = GameObject.Find("First Person Player").GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.IncreaseEnemyCount();
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.position - transform.position).sqrMagnitude <= sightRange * sightRange)
        {
            chasePlayer = true;
        }

        if ((player.position - transform.position).sqrMagnitude <= attackRange * attackRange)
        {
            playerHealth.TakeDamage(30);
            DestroyEnemy();
        }

        if(chasePlayer)
        {
            animator.SetFloat("speed", 1f);
            transform.LookAt(player);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), .1f);
        }
    }

    void DestroyEnemy()
    {
        gameManager.DecreaseEnemyCount();
        Destroy(gameObject);
    }
}
