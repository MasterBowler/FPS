using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health = 100f;
    public float bulletDamage = 5f;

    public HealthBar healthBar;

    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0f, maxHealth);
        healthBar.SetHealth(health);
        if (health == 0)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            health -= bulletDamage;
            health = Mathf.Clamp(health, 0f, maxHealth);
            healthBar.SetHealth(health);
            if ( health == 0)
            {
                FindObjectOfType<GameManager>().EndGame();
            }
        }
    }
}
