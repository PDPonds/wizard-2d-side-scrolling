using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatable
{
    public int curHP { get; set; }

    public void SetHP(int amount);

    public void Heal(int amount);

    public void TakeDamage(int amount);

    public void Dead();

}
