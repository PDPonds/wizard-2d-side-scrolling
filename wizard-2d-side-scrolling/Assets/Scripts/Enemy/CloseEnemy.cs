using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/CloseEnemy")]
public class CloseEnemy : EnemySO
{
    public Vector2 attackOffset;

    public CloseEnemy()
    {
        enemyType = EnemyType.CloseAttack;
    }
}
