using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard
{  
    public Card Card { get; }  

    public int Attack { get; }
    public int Def { get; }
    public int Health { get; }
    public int Level { get; }
    public RaceCard Race { get; }

    public int BonusAttackSkill { get; }
    
    public void TakeDamage(int damage);
}
