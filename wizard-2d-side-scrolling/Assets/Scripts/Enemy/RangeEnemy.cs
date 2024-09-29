using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Enemy/RangeEnemy")]
public class RangeEnemy : EnemySO
{
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float bulletDuration;

    public RangeEnemy()
    {
        enemyType = EnemyType.RangeAttack;
    }
}
