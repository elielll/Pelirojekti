using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    public float speed = 2f;
    private GameObject player;
    private bool chase = false;
    public Transform StartingPoint;
    public int batHealth = 100;
    int currentHealth;
    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = batHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        anim.SetTrigger("isHurt");

        if(currentHealth <= 0)
        {
            anim.SetBool("isDead", true);
            GetComponent<EnemyBat>().enabled = false;
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
        if (player == null)
            return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        
        if (distance < 10)
            chase = true;
        else
            chase = false;
        
        if (chase == true)
            Chase();
        else
            ReturnStartingPoint();

        Flip();
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void Flip()
    {
        if(transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void ReturnStartingPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, StartingPoint.transform.position, speed * Time.deltaTime);
    }
}

