using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceCardStatistic : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _atk, _def, _rarity, _race, _name, _health;

    [SerializeField] private Slider _levelPointsSlider;

    private CardCellInDeck _card;

    public void Render(EnchanceCardCell cardForUpgrade)
    {
        Render(cardForUpgrade);

        _levelPointsSlider.value = 0;
        _levelPointsSlider.maxValue = 100;
    }

    public void Render(CardCell cardForDelete)
    {
        _icon.sprite = cardForDelete.Card.UIIcon;

        _atk.text = "Atk: " + cardForDelete.Attack;
        _def.text = "Def: " + cardForDelete.Def;
        _race.text = cardForDelete.Card.Race.ToString();
        _rarity.text = cardForDelete.Card.Rarity.ToString();
        _name.text = cardForDelete.Card.Name;
        _health.text = cardForDelete.Card.Health.ToString();        
    }

    public void ResetCard()
    {
        _card.ResetComponent();
        gameObject.SetActive(false);
    }
}
