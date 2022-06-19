using System.Collections;
using System.Collections.Generic;
using Cards.Deck.CardCell;
using Pages.Collection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatisticWindow : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _atk, _def, _rarity, _race, _name;

    [SerializeField] private CardCollection _cardCollection;

    private CardCellInDeck _card;

    private void OnEnable()
    {
        _cardCollection.gameObject.SetActive(false);
    }

    public void Render(CardCellInDeck cardCell)
    {
        _icon.sprite = cardCell.Card.UIIcon;

        _atk.text = "Atk: " + cardCell.Attack.ToString();
        _def.text = "Def: " + cardCell.Def.ToString();
        _race.text = cardCell.Card.Race.ToString();
        _rarity.text = cardCell.Card.Rarity.ToString();
        _name.text = cardCell.Card.Name.ToString();

        _card = cardCell;
    }

    public void ResetCard()
    {
        _card.ResetComponent();
        gameObject.SetActive(false);
    }
}
