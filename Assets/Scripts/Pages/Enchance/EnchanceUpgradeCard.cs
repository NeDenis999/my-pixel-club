using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnchanceUpgradeCard : MonoBehaviour
{    
    [SerializeField] private EnchanceCardCollection _enhanceCardCollection;    

    [SerializeField] private Image _UIIcon;
    [SerializeField] private Sprite _standardSprite;

    private EnhanceCardForUpgradeStatistic _cardStatistic;

    private CardCollectionCell _cardCell;
    public CardCollectionCell CardCell => _cardCell;

    private void Start()
    {
        _cardStatistic = FindObjectOfType<EnhanceCardForUpgradeStatistic>().gameObject.GetComponent<EnhanceCardForUpgradeStatistic>();
    }

    private void OnEnable()
    {        
        GetComponent<Button>().onClick.AddListener(OpenCardCollection);
        Reset();
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(OpenCardCollection);
    }

    private void OpenCardCollection()
    {
        _enhanceCardCollection.gameObject.SetActive(true);
        _enhanceCardCollection.UpgradeCard = this;
    }

    private void Reset()
    {
        _cardCell = null;
        _UIIcon.sprite = _standardSprite;
    }

    public void SetCardForUpgrade(CardCollectionCell card)
    {
        if (card == null) throw new System.ArgumentNullException();

        _cardCell = card;
        _UIIcon.sprite = CardCell.UIIcon;
        _cardStatistic.Render(this);
        
        print(card.LevelPoint);
    }
}
