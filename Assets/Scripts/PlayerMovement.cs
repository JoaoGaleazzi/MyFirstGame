using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;

    private Rigidbody2D body;
    private Animator animator;
    private bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        animator.SetBool("Walking", horizontalInput != 0);
        if (horizontalInput != 0)
        {
            body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);
            transform.localScale = new Vector2(horizontalInput * System.Math.Abs(transform.localScale.x), transform.localScale.y);
        }
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        grounded = false;
        body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        animator.SetTrigger("Jump");
        animator.SetBool("Grounded", grounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
            animator.SetBool("Grounded", grounded);
        }
    }
}
