using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Evolution : MonoBehaviour
{
    public event UnityAction OnEvolvedCard;

    [SerializeField] private CardCollection _cardCollection;
    [SerializeField] private EvolveCardCollection _evolveCardCollection;

    [SerializeField] private EvolutionCard _firstCardForEvolution, _secondeCardForEvolution;    

    [SerializeField] private Button _evolveButton;

    [SerializeField] private GameObject _exeptionWindow;

    [SerializeField] private GameObject _evolvedCardWindow;
    [SerializeField] private Image _evolvedCardImage;

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
            _cardCollection.AddCard(GetEvolvedCard());
            _cardCollection.DeleteCards(new[] { FirstCard.CardCell, SecondeCard.CardCell });
            OnEvolvedCard?.Invoke();
        }
        else
        {
            _exeptionWindow.SetActive(true);
        }
    }

    private Card GetEvolvedCard()
    {
        Card evolvedCard = Instantiate(_firstCardForEvolution.CardCell.Card);

        evolvedCard.Evolve(_firstCardForEvolution, _secondeCardForEvolution);

        _evolvedCardWindow.SetActive(true);
        _evolvedCardImage.sprite = evolvedCard.UIIcon;

        return evolvedCard;
    }
}

