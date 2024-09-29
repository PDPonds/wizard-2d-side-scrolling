using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum EnemyBehavior
{
    Idle, Chase, Dead
}

public class EnemyController : MonoBehaviour, ICombatable
{
    [SerializeField] LayerMask attackMask;
    [SerializeField] Transform attackPoint;

    public EnemySO enemy;

    [SerializeField] EnemyBehavior behavior;

    public int curHP { get; set; }

    float curDelay;

    SpriteRenderer rend;
    Animator anim;
    Rigidbody2D rb;
    Collider2D col;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = rb.GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        SetHP(enemy.enemyHP);
        anim.runtimeAnimatorController = enemy.animOverride;
        SwitchBehavior(EnemyBehavior.Idle);
    }

    private void Update()
    {
        DecreasAttactDelay();
        UpdateBehavior();
    }

    public void SetHP(int amount)
    {
        curHP = amount;
    }

    public void Heal(int amount)
    {
        curHP += amount;
        if (curHP > enemy.enemyHP)
        {
            curHP = enemy.enemyHP;
        }
    }

    public void TakeDamage(int amount)
    {
        curHP -= amount;
        if (curHP <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        SwitchBehavior(EnemyBehavior.Dead);
    }

    public void SwitchBehavior(EnemyBehavior behavior)
    {
        this.behavior = behavior;
        switch (this.behavior)
        {
            case EnemyBehavior.Idle:
                break;
            case EnemyBehavior.Chase:
                break;
            case EnemyBehavior.Dead:
                break;
        }
    }

    void UpdateBehavior()
    {
        switch (behavior)
        {
            case EnemyBehavior.Idle:
                Collider2D col = Physics2D.OverlapCircle(transform.position, enemy.checkPlayerRange, attackMask);
                if (col != null)
                {
                    SwitchBehavior(EnemyBehavior.Chase);
                }
                break;
            case EnemyBehavior.Chase:
                CheckDistanceToAttack();

                int dir = GetMoveDir();
                if (dir > 0) rend.flipX = false;
                else if (dir < 1) rend.flipX = true;

                break;
            case EnemyBehavior.Dead:
                rb.velocity = Vector3.zero;
                anim.Play("Death");
                Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
                break;
        }
    }

    public bool IsBehavoir(EnemyBehavior behavior)
    {
        return this.behavior == behavior;
    }

    void DecreasAttactDelay()
    {
        if (curDelay > 0)
        {
            curDelay -= Time.deltaTime;
            if (curDelay <= 0)
            {
                curDelay = 0;
            }
        }
    }

    void CheckDistanceToAttack()
    {
        float dist = Vector2.Distance(transform.position, GameManager.Instance.player.transform.position);
        if (dist < enemy.attackRange)
        {
            if (curDelay == 0)
            {
                Attack();
            }
        }
        else
        {
            int dir = GetMoveDir();
            rb.velocity = new Vector3(dir * enemy.moveSpeed, rb.velocity.y);

        }

    }

    int GetMoveDir()
    {
        if (GameManager.Instance.player.transform.position.x - transform.position.x > 0)
        {
            return 1;
        }
        else if (GameManager.Instance.player.transform.position.x - transform.position.x < 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }

    }

    void Attack()
    {
        rb.velocity = Vector2.zero;
        anim.Play("Attack");

        if (enemy is CloseEnemy close)
        {
            Collider2D col = Physics2D.OverlapCircle((Vector2)transform.position + close.attackOffset, close.attackRange, attackMask);
            if (col != null)
            {
                if (col.TryGetComponent<ICombatable>(out ICombatable icombat))
                {
                    icombat.TakeDamage(enemy.enemyDmg);
                }
            }
        }
        else if (enemy is RangeEnemy range)
        {
            PlayerManager player = GameManager.Instance.player;
            Vector2 playerPos = player.transform.position;
        }
        curDelay = enemy.enemyAttackDelay;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (enemy != null)
        {
            if (enemy is CloseEnemy close)
            {
                Gizmos.DrawWireSphere((Vector2)transform.position + close.attackOffset, close.attackRange);
            }
        }

        Gizmos.DrawWireSphere(transform.position, enemy.checkPlayerRange);

    }

}
