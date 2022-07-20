using System.Collections;
using System.Collections.Generic;
using Infrastructure.Services;
using FarmPage.Evolve;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EvolutionCard : MonoBehaviour
{
    [SerializeField] private EvolveCardCollection _evolveCardCollection;

    [SerializeField] private Evolution _evolution;

    [SerializeField] private Image _UIIcon;
    [SerializeField] private Sprite _standardSprite;
    [SerializeField] private Image _frame;
    
    public bool IsSet => _isSet;
    private bool _isSet = false;
    private AssetProviderService _assetProviderService;
    
    public CardCollectionCell CardCell { get; private set; }

    [Inject]
    private void Construct(AssetProviderService assetProviderService)
    {
        _assetProviderService = assetProviderService;
    }
    
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

    public void SetCard(CardCollectionCell selectCard)
    {
        CardCell = selectCard;
        _UIIcon.sprite = CardCell.UIIcon;
        _frame.gameObject.SetActive(true);
        _frame.sprite = CardCell.Card.GetFrame();
        _isSet = true;
    }

    public void Reset()
    {
        CardCell = null;
        _UIIcon.sprite = _standardSprite;
        _frame.gameObject.SetActive(false);
        _isSet = false;
    }

    private void OpenCollectionCard()
    {
        _evolveCardCollection.gameObject.SetActive(true);
        _evolveCardCollection.OneOfCardInEvolutioin = this;
    }
}
