using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EnchanceCardForDeleteCell : CardCollectionCell
{
    [SerializeField] private GameObject _selectPanel;

    private EnchanceCardsForDeleteCollection _enchanceCardForDeleteCollection;
    private CardCollectionCell _cardInCollection;

    private Enchance _enchance;

    private Button _cardButton;
    private Button _selectedPanelButton;

    private EnhanceCardForDeleteStatistic _cardStatistic;

    override protected void Awake()
    {
        _enchanceCardForDeleteCollection = FindObjectOfType<EnchanceCardsForDeleteCollection>().gameObject.GetComponent<EnchanceCardsForDeleteCollection>();
        _enchance = FindObjectOfType<Enchance>().gameObject.GetComponent<Enchance>();

        _cardStatistic = FindObjectOfType<EnhanceCardForDeleteStatistic>().gameObject.GetComponent<EnhanceCardForDeleteStatistic>();

        _cardButton = GetComponent<Button>();
        _selectedPanelButton = _selectPanel.GetComponent<Button>();
    }

    private void OnEnable()
    {
        _cardButton.onClick.AddListener(SelectCard);
        _selectedPanelButton.onClick.AddListener(UnselectCard);
        _selectPanel.SetActive(false);
    }

    private void OnDisable()
    {
        _cardButton.onClick.RemoveListener(SelectCard);
        _selectedPanelButton.onClick.RemoveListener(UnselectCard);
    }

    public void SetLinkOnCardInCollection(CardCollectionCell cardInCollection)
    {
        if (cardInCollection == null) throw new System.NullReferenceException();

        _cardInCollection = cardInCollection;
    }

    private void SelectCard()
    {
        if (_enchance.UpgradeCard.CardCell.Level + _enchanceCardForDeleteCollection.PosibleLevelUpSlider.HowMuchIncreaseLevel < 25)
        {
            _enchanceCardForDeleteCollection.AddToDeleteCollection(_cardInCollection);
            _selectPanel.SetActive(true);
            _cardStatistic.Render(_cardInCollection);
        }
    }

    private void UnselectCard()
    {
        _enchanceCardForDeleteCollection.RetrieveCard(_cardInCollection);
        _selectPanel.SetActive(false);
    }
}
