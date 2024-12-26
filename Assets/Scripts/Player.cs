using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    Animator _animator;
    Rigidbody2D _rigidbody;

    public float moveSpeed = 5.0f;
    public float jumpForce = 20.0f;

    private bool isGrounded = true;

    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    public float groundCheckDistance = 0.5f;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected bool CheckIsGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(
            transform.position,
            groundCheckRadius,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        return hit.collider != null;
    }

    public virtual void Jump()
    {
        if (!CheckIsGrounded()) return;

        _rigidbody.velocity = Vector2.zero;
        transform.Translate(Vector2.up * 0.4f);
        _rigidbody.AddForce(jumpForce * Vector2.up);
    }

    public virtual void MoveLeft()
    {
        transform.Translate(moveSpeed * Vector2.left * Time.deltaTime);
    }

    public virtual void MoveRight()
    {
        transform.Translate(moveSpeed * Vector2.right * Time.deltaTime);
    }

    public virtual void Attack()
    {

    }

    public virtual void Crouch()
    {
        bool isCrouching = _animator.GetBool("Crouch");
        _animator.SetBool("Crouch", !isCrouching);
    }
}
