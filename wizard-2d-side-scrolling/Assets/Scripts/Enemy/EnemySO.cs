using System;
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

    [Header("===== Attack =====")]
    [Header("- Damage")]
    public int enemyDmg;
    [Header("- Delay")]
    public int enemyAttackDelay;
    [Header("- Range")]
    public float attackRange;

    public EnemyType enemyType;

    [Header("===== Skill =====")]
    public List<SkillSlot> skills = new List<SkillSlot>();

    public void ActiveDeathSkill(GameObject owner)
    {
        if (skills.Count > 0)
        {
            for (int i = 0; i < skills.Count; i++)
            {
                if (skills[i].skillType == SkillType.OnDeath)
                {
                    skills[i].enemySkill.ActivateSkill(owner);
                }
            }
        }
    }

    public void ActiveAttackSkill(GameObject owner)
    {
        if (skills.Count > 0)
        {
            for (int i = 0; i < skills.Count; i++)
            {
                if (skills[i].skillType == SkillType.OnAttack)
                {
                    skills[i].enemySkill.ActivateSkill(owner);
                }
            }
        }
    }

    public void ActiveSpawnSkill(GameObject owner)
    {
        if (skills.Count > 0)
        {
            for (int i = 0; i < skills.Count; i++)
            {
                if (skills[i].skillType == SkillType.OnSpawn)
                {
                    skills[i].enemySkill.ActivateSkill(owner);
                }
            }
        }
    }

}

public enum SkillType
{
    OnDeath, OnAttack, OnSpawn
}

[Serializable]
public class SkillSlot
{
    public EnemySkill enemySkill;
    public SkillType skillType;
}