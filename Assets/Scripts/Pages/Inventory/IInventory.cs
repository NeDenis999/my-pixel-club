using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    public Sprite UIIcon { get; }
    public BottleEffects Effect { get; }

    void UseEffect(Inventory inventory, InventoryCell inventoryCell);
}
