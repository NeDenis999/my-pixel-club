using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InventoryCell : MonoBehaviour
{
    public event UnityAction<InventoryCell, ShopItemBottle> OnUsedEnergyBottle;

    [SerializeField] private TMP_Text _amountThisItemText;

    [SerializeField] private Image _icon;

    private BottleEffects _effects;

    private ShopItemBottle _shopItemBottle;
    private InventoryConfirmWindow _confirmWindow;

    private int _amountThisItem = 1;

    public ShopItemBottle Item => _shopItemBottle;

    public int AmountThisItem
    {
        get => _amountThisItem;
        set
        {
            _amountThisItem = value;
            _amountThisItemText.text = _amountThisItem.ToString();
        }
    }

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(UseEffect);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(UseEffect);
    }

    public void Render(ShopItemBottle shopItem, Inventory inventory)
    {
        _icon.sprite = shopItem.UIIcon;
        _effects = shopItem.Effect;
        _shopItemBottle = shopItem;
        _confirmWindow = inventory.ConfirmWindow;
    }

    private void UseEffect()
    {
        if (_effects == BottleEffects.ReplenishEnergy)
            _confirmWindow.Open(this);
    }
}
