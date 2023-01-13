using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public LayerMask groundLayers;
    public Transform groundCheck;
    bool isFacingRight = true;
    RaycastHit2D hit;
    public int maxHealth = 100;
    int currentHealth;
    private Animator anim;
    void Start()
    {
        anim =GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        anim.SetTrigger("isHurt");

        if(currentHealth <= 0)
        {
            anim.SetBool("isDead", true);
            GetComponent<EnemyPatrol>().enabled = false;
            Destroy(GetComponent<Collider2D>());
            this.enabled = false;

        }
    }
    

    void Die()
    {
        Destroy(this.gameObject);
    }

    private void Update()
    {
        hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayers);
    }
    
    private void FixedUpdate()
    {
        if (hit.collider != false)
        {
            if (isFacingRight)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            } else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        } else
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(-transform.localScale.x, 6f, 6f);
        }
    }
}
