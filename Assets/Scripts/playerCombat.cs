using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    private Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 50;
    
    private void Start() 
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }
    }
    void Attack()
    {
        anim.SetTrigger("isAttacking");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy.gameObject.CompareTag("EnemyPatrol"))
            {
            enemy.GetComponent<EnemyPatrol>().TakeDamage(attackDamage);
            }
            if(enemy.gameObject.CompareTag("EnemyBat"))
            {
            enemy.GetComponent<EnemyBat>().TakeDamage(attackDamage);
            }
            if(enemy.gameObject.CompareTag("GoblinEnemy"))
            {
            enemy.GetComponent<GoblinEnemy>().TakeDamage(attackDamage);
            }
            if(enemy.gameObject.CompareTag("Boss"))
            {
            enemy.GetComponent<Boss>().TakeDamage(attackDamage);
            }
 
        }
   
    }

    private void OnDrawGizmosSelected() 
    {
        if(attackPoint == null)
        return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
