using System.Collections.Generic;
using Infrastructure.Services;
using Pages.Enhance.Card_Statistic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Pages.Enhance
{
    public class Enchance : MonoBehaviour
    {
        [SerializeField] private CardCollection _cardCollection;
        [SerializeField] private EnchanceCardCollection _enhanceCardForUpgradeCollection;
        [SerializeField] private EnchanceCardsForDeleteCollection _enhanceCardsForDeleteCollection;
        [SerializeField] private EnchanceUpgradeCard _upgradeCard;
        [SerializeField] private EnhanceCardForUpgradeStatistic _upgradeCardStatistic;
        [SerializeField] private Button _enhanceButton;
        [SerializeField] private GameObject _exeptionWindow;
        [SerializeField] private PossibleLevelUpSlider _possibleLevelUpSlider;

        private DataSaveLoadService _dataSaveLoadService;
        
        public EnchanceUpgradeCard UpgradeCard => _upgradeCard;

        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService)
        {
            _dataSaveLoadService = dataSaveLoadService;
        }
    
        private void OnEnable()
        {
            _enhanceCardForUpgradeCollection.InitCardCollection(_cardCollection.Cards);

            _enhanceButton.onClick.AddListener(Enhance);
        }

        private void OnDisable()
        {
            _enhanceButton.onClick.RemoveListener(Enhance);
        }

        private void Enhance()
        {
            List<CardCollectionCell> currentEnhanceCardList = new();

            if (_enhanceCardsForDeleteCollection.CardForDelete.Count == 0)
            {
                _exeptionWindow.SetActive(true);
                return;
            }

            _possibleLevelUpSlider.Reset();

            if (_upgradeCard.CardCell == null) return;

            _upgradeCard.CardCell.LevelUp(_enhanceCardsForDeleteCollection.CardForDelete.ToArray());
            _upgradeCardStatistic.Render(_upgradeCard);
            _cardCollection.DeleteCards(_enhanceCardsForDeleteCollection.CardForDelete.ToArray());

            currentEnhanceCardList.AddRange(_cardCollection.Cards);
            currentEnhanceCardList.Remove(_upgradeCard.CardCell);

            _enhanceCardForUpgradeCollection.InitCardCollection(currentEnhanceCardList);
            _enhanceCardsForDeleteCollection.DisplayCardsForDelete(currentEnhanceCardList);

            _dataSaveLoadService.SetInventoryDecks(_cardCollection.Cards);
        }
    }
}
