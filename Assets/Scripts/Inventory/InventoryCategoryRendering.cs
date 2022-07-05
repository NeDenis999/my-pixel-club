using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryCategoryRendering : MonoBehaviour
{
    [SerializeField] protected Transform _container;
    [SerializeField] protected InventoryCell _inventoryItemCellTemplate;
}
