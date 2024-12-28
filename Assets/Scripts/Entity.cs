using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    Animator _animator;
    Rigidbody2D _rigidbody;
    SpriteRenderer _spriteRenderer;

    public Action onDeath;

    public float moveSpeed = 5.0f;
    public float defaultSpeed = 5.0f;
    public float crouchSpeed = 2.0f;

    public float jumpForce = 600.0f;
    private bool isJumping = false;
    private float jumpTime = 0.2f;
    public float jumpAccelation = 0.15f;
    public int jumpNumber = 2;
    private int jumpCount = 0;

    private float health = 100f;
    private bool isImmue = false;
    public float immueTime = 1f;

    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    public float groundCheckDistance = 0.5f;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected bool CheckIsGrounded()
    {
        RaycastHit2D hit1 = Physics2D.CircleCast(
            transform.position + new Vector3(0.4f, 0, 0),
            groundCheckRadius,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        RaycastHit2D hit2 = Physics2D.CircleCast(
            transform.position + new Vector3(-0.4f, 0, 0),
            groundCheckRadius,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        bool isGrounded = hit1.collider != null || hit2.collider != null;

        if (isGrounded) jumpCount = 0;

        return isGrounded;
    }

    public virtual void Jump()
    {
        if (!CheckIsGrounded() && jumpCount == 0) return;
        if (isJumping) return;
        if (jumpCount >= jumpNumber) return;

        jumpCount++;

        SetJumpTime();

        _animator.SetTrigger("Jump");

        _rigidbody.velocity = Vector2.zero;
        transform.Translate(Vector2.up * jumpAccelation);
        _rigidbody.AddForce(jumpForce * Vector2.up);
    }

    public async void SetJumpTime()
    {
        isJumping = true;
        await Task.Delay((int)(jumpTime * 1000));
        isJumping = false;    
    }


    public virtual void MoveLeft()
    {
        _spriteRenderer.flipX = true;

        transform.Translate(moveSpeed * Vector2.left * Time.deltaTime);
    }

    public virtual void MoveRight()
    {
        _spriteRenderer.flipX = false;

        transform.Translate(moveSpeed * Vector2.right * Time.deltaTime);
    }

    public virtual void MoveAnimation()
    {
        bool isWalking = _animator.GetBool("Walk");
        _animator.SetBool("Walk", !isWalking);
    }

    public virtual void Attack()
    {

    }

    public virtual void Crouch()
    {
        bool isCrouching = _animator.GetBool("Crouch");
        _animator.SetBool("Crouch", !isCrouching);

        if (isCrouching) moveSpeed = defaultSpeed;
        else moveSpeed = crouchSpeed;
    }

    public virtual void TakeDamage(float damage)
    {
        if (isImmue) return;

        _animator.SetTrigger("Damage");

        health -= damage;
        SetImmueTime();

        if (health <= 0)
        {
            onDeath();
            Die();
        }
    }

    public async void SetImmueTime()
    {
        isImmue = true;
        await Task.Delay((int)(immueTime * 1000));
        isImmue = false;    
    }

    protected virtual void Die()
    {
        // Die
    }
}
