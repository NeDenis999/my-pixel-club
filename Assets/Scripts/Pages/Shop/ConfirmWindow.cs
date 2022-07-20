using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace FarmPage.Shop
{
    public class ConfirmWindow : MonoBehaviour 
    {
        private const int MaxCountCard = 50;
        
        [SerializeField]
        private TMP_Text _quantityMoneyToBuy;

        [SerializeField] 
        private TMP_Text _itemType;
        
        [SerializeField] 
        private TMP_Text _descriptionText;

        [SerializeField] 
        private TMP_Text _countText;

        [SerializeField] 
        private global::Shop _shop;
        
        [SerializeField] 
        private Image _iconImage;

        [SerializeField] 
        private TMP_Dropdown _amountItems;

        [SerializeField] 
        private GameObject _errorWindow;

        [SerializeField] 
        private ConfirmWindowAnimator _confirmWindowAnimator;

        [SerializeField] private GoldWallet _goldWallet;

        private ShopItem _shopItem;
        private DataSaveLoadService _dataSaveLoadService;
        
        

        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService)
        {
            _dataSaveLoadService = dataSaveLoadService;
        }
    
        public void Render(ShopItem item)
        {
            _amountItems.value = 0;

            _shopItem = item;
            _quantityMoneyToBuy.text = item.name + '\n' + "Price: " + item.Price.ToString();
            _itemType.text = item.name;
            _iconImage.sprite = item.UIIcon;
            _descriptionText.text = item.name;
            _countText.text = $"x1";
        }

        public void Buy()
        {
            if (_shopItem.Item is ShopItemCardPack && _dataSaveLoadService.AmountCards + (int)_shopItem.TypeItem * (_amountItems.value + 1) > MaxCountCard)
            {
                _errorWindow.SetActive(transform);
                return;
            }

            for (int i = 0; i <= _amountItems.value; i++)
            {
                if (_shopItem.Item is ShopItemCardPack)
                    _shop.BuyCard((ShopItemCardPack)_shopItem);
                else
                    _shop.BuyItem(_shopItem);
            
                _goldWallet.WithdrawÑurrency(_shopItem.Price);
            }

            _confirmWindowAnimator.PlayBuyAnimation();
        }
    }
}
