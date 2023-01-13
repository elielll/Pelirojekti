using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ladderMovement : MonoBehaviour
{
    private float vertical;
    private bool isLadder;
    private bool isClimbing;
    public Animator animator;

    [SerializeField] private Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {   
            isClimbing = true;
        }
    }
    private void FixedUpdate()
    {

        if (rigidbody2d == null)
            return;

        if (isClimbing)
        {
            animator.SetBool("isClimbing", true);
            rigidbody2d.gravityScale = 0f;
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, vertical * 8f);
        }
        else
        {
            rigidbody2d.gravityScale = 1f;
            animator.SetBool("isClimbing", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}
