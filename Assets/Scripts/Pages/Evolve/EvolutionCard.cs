using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionCard : MonoBehaviour
{
    [SerializeField] private EvolveCardCollection _cardCollection;

    [SerializeField] private Evolution _evolution;

    [SerializeField] private Image _UIIcon;
    [SerializeField] private Sprite _standardSprite;

    public bool IsSet => _isSet;
    private bool _isSet = true;

    public CardCollectionCell CardCell { get; private set; }

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OpenCollectionCard);
        _evolution.OnEvolvedCard += Reset;
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(OpenCollectionCard);
        _evolution.OnEvolvedCard += Reset;
    }

    private void OpenCollectionCard()
    {
        _cardCollection.gameObject.SetActive(true);
        _cardCollection.OneOfCardInEvolutioin = this;
    }

    private void Reset(Card card)
    {
        CardCell = null;
        _UIIcon.sprite = _standardSprite;
        _isSet = false;
    }

    public void SetCard(CardCollectionCell selectCard)
    {
        CardCell = selectCard;
        _UIIcon.sprite = CardCell.UIIcon;
        _isSet = true;
    }
}
