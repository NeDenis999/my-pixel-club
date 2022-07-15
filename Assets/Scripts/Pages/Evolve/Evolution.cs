using Data;
using Infrastructure.Services;
using Pages.Evolve;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class Evolution : MonoBehaviour
{
    private const float ValueIncreaseMultiplier = 1.35f;
    private const float ValueLevelUpIncreaseMultiplier = 1.15f;

    [SerializeField] private CardCollection _cardCollection;
    [SerializeField] private EvolveCardCollection _evolveCardCollection;

    [SerializeField] private EvolutionCard _firstCardForEvolution, _secondeCardForEvolution;    

    [SerializeField] private Button _evolveButton;
    [SerializeField] private Button _resetButton;

    [SerializeField] private GameObject _exeptionWindow;

    [SerializeField] private GameObject _evolvedCardWindow;
    [SerializeField] private Image _evolvedCardImage;

    private DataSaveLoadService _dataSaveLoadService;
    private AssetProviderService _assetProviderService;
    
    public event UnityAction OnEvolvedCard;
    public EvolutionCard FirstCard => _firstCardForEvolution;
    public EvolutionCard SecondeCard => _secondeCardForEvolution;

    [Inject]
    private void Construct(DataSaveLoadService dataSaveLoadService, AssetProviderService assetProviderService)
    {
        _dataSaveLoadService = dataSaveLoadService;
        _assetProviderService = assetProviderService;
    }
    
    private void OnEnable()
    {
        _evolveCardCollection.SetCardCollection(_cardCollection.Cards);

        _resetButton.onClick.AddListener(Reset);
        _evolveButton.onClick.AddListener(EvolveCard);
    }

    private void OnDisable()
    {
        _resetButton.onClick.RemoveListener(Reset);
        _evolveButton.onClick.RemoveListener(EvolveCard);
    }

    public void Reset()
    {
        _firstCardForEvolution.Reset();
        _secondeCardForEvolution.Reset();

        _evolveCardCollection.SetCardCollection(_cardCollection.Cards);
    }

    private void EvolveCard()
    {
        if (_firstCardForEvolution.IsSet && _secondeCardForEvolution.IsSet)
        {
            _cardCollection.AddCard(GetEvolvedCard());
            _cardCollection.DeleteCards(new[] { FirstCard.CardCell, SecondeCard.CardCell });
            _dataSaveLoadService.SetInventoryDecks(_cardCollection.Cards);
            OnEvolvedCard?.Invoke();
        }
        else
        {
            _exeptionWindow.SetActive(true);
        }
    }

    private CardData GetEvolvedCard()
    {
        CardData evolvedCard = Evolve(_firstCardForEvolution, _secondeCardForEvolution);

        _evolvedCardWindow.SetActive(true);
        _evolvedCardImage.sprite = _assetProviderService.AllCards[evolvedCard.Id].ImageSecondeEvolution;

        return evolvedCard;
    }
    
    private CardData Evolve(EvolutionCard firstCard, EvolutionCard secondCard)
    {
        CardData evolvedCard = new CardData();

        evolvedCard.Attack = GetEvolveUpValue(firstCard.CardCell.CardData.Attack, secondCard.CardCell.CardData.Attack);
        evolvedCard.Defence = GetEvolveUpValue(firstCard.CardCell.CardData.Defence, secondCard.CardCell.CardData.Attack);
        evolvedCard.Health = GetEvolveUpValue(firstCard.CardCell.CardData.Health, secondCard.CardCell.CardData.Attack);
        evolvedCard.Id = firstCard.CardCell.CardData.Id;
        evolvedCard.Evolution = 2;
        evolvedCard.Level = 1;
        evolvedCard.MaxLevelPoint = 1000;

        return evolvedCard;
    }
    
    private int GetEvolveUpValue(int firstValue, int secondValue)
    {
        var average = (firstValue + secondValue) / 2;
        var evolveUpValue = average * ValueIncreaseMultiplier;
        return (int)evolveUpValue;
    }
}

