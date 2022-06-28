using Pages.Collection;
using System.Collections;
using System.Collections.Generic;
using Pages.Evolve;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionCard : MonoBehaviour
{
    [SerializeField] private EvolveCardCollection _evolveCardCollection;

    [SerializeField] private Evolution _evolution;

    [SerializeField] private Image _UIIcon;
    [SerializeField] private Sprite _standardSprite;

    public bool IsSet => _isSet;
    private bool _isSet = false;

    public CardCollectionCell CardCell { get; private set; }

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OpenCollectionCard);
        _evolution.OnEvolvedCard += Reset;
        Reset();
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(OpenCollectionCard);
        _evolution.OnEvolvedCard -= Reset;
    }

    private void OpenCollectionCard()
    {
        _evolveCardCollection.gameObject.SetActive(true);
        _evolveCardCollection.OneOfCardInEvolutioin = this;
    }

    private void Reset()
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
