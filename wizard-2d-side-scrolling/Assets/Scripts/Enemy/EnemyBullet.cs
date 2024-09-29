using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    int damage;

    Animator anim;
    Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetupDamage(int dmg)
    {
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            anim.Play("Hit");
            rb.velocity = Vector3.zero;
            if (collision.TryGetComponent<ICombatable>(out ICombatable ICom))
            {
                ICom.TakeDamage(damage);
            }
        }
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9)
        {
            Destroy(gameObject);
        }
    }
}
