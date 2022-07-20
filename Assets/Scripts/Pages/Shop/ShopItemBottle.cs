using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BottleEffects
{
    None,
    ReplenishEnergy,
    ReplenishHealth
}

[CreateAssetMenu(fileName = "Bottle", menuName = "ScriptableObjects/Shop/Bottle")]
public class ShopItemBottle : ShopItem, IInventory
{
    [SerializeField] private BottleEffects _bottleEffects;

    public BottleEffects Effect => _bottleEffects;
    public int Id;

    public void UseEffect()
    {
        throw new NotImplementedException();
    }
}
