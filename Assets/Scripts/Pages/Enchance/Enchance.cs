using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enchance : MonoBehaviour
{
    [SerializeField] private CardCollection _cardCollection;
    [SerializeField] private EnchanceCardCollection _enhanceCardForUpgradeCollection;
    [SerializeField] private EnchanceCardsForDeleteCollection _enhanceCardsForDeleteCollection;

    [SerializeField] private EnchanceUpgradeCard _upgradeCard;
    [SerializeField] private EnhanceCardForUpgradeStatistic _upgradeCardStatistic;

    [SerializeField] private Button _enhanceButton;

    [SerializeField] private GameObject _exeptionWindow;

    private void OnEnable()
    {
        _enhanceCardForUpgradeCollection.SetCardCollection(_cardCollection.Cards);

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

        if (_upgradeCard.CardCell == null) return;

        //_cardCollection.DeleteCards(new[] { _upgradeCard.CardCell });
        _upgradeCard.CardCell.LevelUp(_enhanceCardsForDeleteCollection.CardForDelete.ToArray());
        _upgradeCardStatistic.Render(_upgradeCard.CardCell);
        _cardCollection.DeleteCards(_enhanceCardsForDeleteCollection.CardForDelete.ToArray());

        currentEnhanceCardList.AddRange(_cardCollection.Cards);
        currentEnhanceCardList.Remove(_upgradeCard.CardCell);

        _enhanceCardForUpgradeCollection.SetCardCollection(currentEnhanceCardList);
        _enhanceCardsForDeleteCollection.DisplayCardsForDelete(currentEnhanceCardList);
    }
}
