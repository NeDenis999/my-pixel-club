using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public event UnityAction<InventoryCell, ShopItemBottle> OnUsedEnergyBottle;

    [SerializeField] private TMP_Text _amountThisItemText;

    [SerializeField] private Image _icon;
    [SerializeField] private Inventory _inventory;

    [SerializeField] private InventoryItemDiscription _statisticWindowTemplayte;

    private string _statistic;
    private string _discription;
    private BottleEffects _effects;

    private ShopItemBottle _shopItemBottle;

    private InventoryItemDiscription _statisticWindow;

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

    public void Render(IInventory item)
    {
        _icon.sprite = item.UIIcon;
        _statistic = item.Statistic;
        _discription = item.Discription;
        _effects = item.Effect;        
    }

    public void Render(ShopItemBottle shopItem)
    {
        Render(shopItem as IInventory);
        _shopItemBottle = shopItem;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var lastestStatisticWindow = FindObjectOfType<InventoryItemDiscription>();
        if(lastestStatisticWindow != null)
            Destroy(lastestStatisticWindow.gameObject);

        _statisticWindow = Instantiate(_statisticWindowTemplayte, gameObject.transform);
        _statisticWindow.RenderDiscription(_statistic, _discription);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UseEffect();
    }
    private void UseEffect()
    {
        if (_effects == BottleEffects.ReplenishEnergy)
        {
            OnUsedEnergyBottle?.Invoke(this, _shopItemBottle);
            _inventory.UseEnergyBottle();
        }
    }
}
