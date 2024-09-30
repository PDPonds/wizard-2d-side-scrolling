using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "EnemySkill/SpawnEnemy")]
public class SpawnEnemySkill : EnemySkill
{
    public EnemySO enemy;
    public int count;

    public override void ActivateSkill(GameObject owner)
    {
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject enemyObj = Instantiate(enemy.enemyPrefab, owner.transform.position, Quaternion.identity);
            }
        }
    }

}
