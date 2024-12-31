using UnityEngine;

public class Dino : Entity
{
    public ParticleSystem dustParticle;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Jump()
    {
        base.Jump();
    }

    public override void MoveLeft()
    {
        base.MoveLeft();
    }

    public override void MoveRight()
    {
        base.MoveRight();
    }

    public override void MoveAnimation()
    {
        base.MoveAnimation();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        base.Die();
    }

    ///

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 착지 시 바닥 충돌 감지
        if (collision.contacts[0].normal.y > 0.5f)
        {
            PlayDustEffect();
        }
    }

    void PlayDustEffect()
    {
        if (dustParticle != null)
        {
            // 먼지 파티클 재생
            dustParticle.transform.position = transform.position + new Vector3(0, -0.3f, 0); // 플레이어 위치로 이동
            dustParticle.Play();
        }
    }
}
