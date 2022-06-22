using Pages.Collection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Evolution : MonoBehaviour
{
    public event UnityAction<Card> OnEvolvedCard;
    public event UnityAction<CardCollectionCell, CardCollectionCell> OnDelitedUseCards;

    [SerializeField] private CardCollection _cardCollection;
    [SerializeField] private EvolveCardCollection _evolveCardCollection;

    [SerializeField] private EvolutionCard _firstCardForEvolution, _secondeCardForEvolution;    

    [SerializeField] private Button _evolveButton;

    [SerializeField] private GameObject _exeptionWindow;

    private Card _evolvedCard;

    public EvolutionCard FirstCard => _firstCardForEvolution;
    public EvolutionCard SecondeCard => _secondeCardForEvolution;

    private void OnEnable()
    {
        _evolveCardCollection.SetCardCollection(_cardCollection.Cards);

        _evolveButton.onClick.AddListener(EvolveCard);
    }

    private void OnDisable()
    {
        _evolveButton.onClick.RemoveListener(EvolveCard);
    }

    private void EvolveCard()
    {
        if (_firstCardForEvolution.IsSet && _secondeCardForEvolution.IsSet)
        {
            OnDelitedUseCards?.Invoke(_firstCardForEvolution.CardCell, _secondeCardForEvolution.CardCell);
            OnEvolvedCard?.Invoke(GetEvolvedCard());
        }
        else
        {
            _exeptionWindow.SetActive(true);
        }
    }

    private Card GetEvolvedCard()
    {
        float avargeAtack = GetAvargeValue(_firstCardForEvolution.CardCell.Attack, _secondeCardForEvolution.CardCell.Attack);
        float avargeDef = GetAvargeValue(_firstCardForEvolution.CardCell.Def, _secondeCardForEvolution.CardCell.Def);
        float avargeHealth = GetAvargeValue(_firstCardForEvolution.CardCell.Health, _secondeCardForEvolution.CardCell.Health);

        _evolvedCard = Instantiate(_firstCardForEvolution.CardCell.Card);

        _evolvedCard.SetEvolutionValue((int)(avargeAtack *= 1.35f),
                                       (int)(avargeDef *= 1.35f),
                                       (int)(avargeHealth *= 1.35f));

        return _evolvedCard;
    }

    private float GetAvargeValue(int firstValue, int secondeValue)
    {
        return (firstValue + secondeValue) / 2;
    }
}
