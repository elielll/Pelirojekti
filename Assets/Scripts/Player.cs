using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    private Rigidbody2D rb;
    public Transform groundPos;
    private bool isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    private Animator anim;
    private int time = 2000;
    int playerHealth = 200;
    public int currentHealth;
    public int TakeDamage = 40;
    public float knockBackx;
    public float knockBacky;
    public float knockBackLenght;
    public float knockBackCount;
    public bool knockFromRight;
    public int trapDamage = 100;
    public HealthBar healthBar;

    private void Start()
    {   
    anim =GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
    currentHealth = playerHealth;
    healthBar.SetMaxHealth(playerHealth);
    }

    private void Update()
    {
    float moveInput = Input.GetAxisRaw("Horizontal");
    rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);


    //kommenteissa osittain toimiva knokback koodi, bugi = knockbakkaa vaa yhteen suuntaan, 
    //pitäs saada toimiin niin että siltä puolelta mistä osutaan niin kimpoaa siitä poispäin.
    //if(knockBackCount <= 0)
    //{
        if(moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        if(moveInput < 0)
        {
            transform.eulerAngles = new Vector3 (0, 180, 0);
        }
        if(moveInput > 0)
        {
            transform.eulerAngles = new Vector3 (0, 0, 0);
        }
    /*}else   {
        if(knockFromRight)
        {
            rb.velocity = new Vector2(-knockBackx, knockBacky);
        }
        if(!knockFromRight)
        {
            rb.velocity = new Vector2(knockBackx, knockBacky);
        }
        knockBackCount -= Time.deltaTime;
    }*/
        isGrounded = Physics2D.OverlapCircle(groundPos.position, checkRadius, whatIsGround);
    if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
    {
        isJumping = true;
        jumpTimeCounter = jumpTime;
        rb.velocity = Vector2.up * jumpForce;
    }

    }

    private void FixedUpdate() 
    {
    if(isGrounded == true)
    {
        anim.SetBool("isJumping", false);
    }
    else
    {
        anim.SetBool("isJumping", true);
    }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Axe"))
        {  
            currentHealth -= trapDamage;     
            healthBar.SetHealth(currentHealth);
            
            anim.SetBool("isDead", true);
            GetComponent<Player>().enabled = false;
            Destroy(rb);
            this.enabled = false;
            
        }


        if (collision.gameObject.CompareTag("EnemyPatrol"))
        {  
            currentHealth -= TakeDamage;
            healthBar.SetHealth(currentHealth);
            anim.SetTrigger("isHurt");
            

            if(currentHealth <= 0)
            {
                anim.SetBool("isDead", true);
                GetComponent<Player>().enabled = false;
                Destroy(rb);
                this.enabled = false;
            }

        }
        if (collision.gameObject.CompareTag("GoblinEnemy"))
        {
            currentHealth -= TakeDamage;
            healthBar.SetHealth(currentHealth);
            anim.SetTrigger("isHurt");
            

            if (currentHealth <= 0)
            {
                anim.SetBool("isDead", true);
                GetComponent<Player>().enabled = false;
                Destroy(rb);
                this.enabled = false;
            }

        }
        if (collision.gameObject.CompareTag("EnemyBat"))
        {
            currentHealth -= TakeDamage;
            healthBar.SetHealth(currentHealth);
            anim.SetTrigger("isHurt");
            

            if (currentHealth <= 0)
            {
                anim.SetBool("isDead", true);
                GetComponent<Player>().enabled = false;
                Destroy(rb);
                this.enabled = false;
            }

        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            currentHealth -= TakeDamage;
            healthBar.SetHealth(currentHealth);
            anim.SetTrigger("isHurt");
            

            if (currentHealth <= 0)
            {
                anim.SetBool("isDead", true);
                GetComponent<Player>().enabled = false;
                Destroy(rb);
                this.enabled = false;
            }
            

        }



            /*knockBackCount = knockBackLenght;
            if(gameObject.transform.position.x < transform.position.x)
            {
                knockFromRight = true;
            }else {
                knockFromRight = false;
            }*/
        }

    private void die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        LevelManager.instance.Respawn();

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Gem"))
        {
            Destroy(col.gameObject);
        }

        if (col.gameObject.CompareTag("Fireball"))
        {
            currentHealth -= trapDamage;
            healthBar.SetHealth(currentHealth);

            anim.SetBool("isDead", true);
            GetComponent<Player>().enabled = false;
            Destroy(rb);
            this.enabled = false;
        }
    }
}
