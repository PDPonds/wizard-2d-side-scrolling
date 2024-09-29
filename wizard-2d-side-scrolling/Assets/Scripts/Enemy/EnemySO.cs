using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    CloseAttack, RangeAttack
}

public class EnemySO : ScriptableObject
{
    public string enemyName;
    public GameObject enemyPrefab;
    public AnimatorOverrideController animOverride;

    [Header("===== HP =====")]
    public int enemyHP;

    [Header("===== Check Range =====")]
    public float checkPlayerRange;

    [Header("===== Walk =====")]
    public float moveSpeed;

    [Header("===== Chase =====")]
    public float chaseTimeWhenPlayerIsClose;

    [Header("===== Attack =====")]
    [Header("- Damage")]
    public int enemyDmg;
    [Header("- Delay")]
    public int enemyAttackDelay;
    [Header("- Range")]
    public float attackRange;

    public EnemyType enemyType;


}
