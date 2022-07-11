using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    public Sprite UIIcon { get; }
    public string Statistic { get; }
    public string Discription { get; }
    public BottleEffects Effect { get; }
}
