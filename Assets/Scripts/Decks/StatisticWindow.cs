using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatisticWindow : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _atk, _def, _rarity, _race, _name, _health;

    [SerializeField] private CardCollection _cardCollection;

    private CardCellInDeck _card;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _cardCollection.gameObject.SetActive(false);
    }

    public void Render(CardCellInDeck cardCell)
    {
        _icon.sprite = cardCell.Card.UIIcon;

        _atk.text = "Atk: " + cardCell.Attack;
        _def.text = "Def: " + cardCell.Def;
        _health.text = "HP: " + cardCell.Health.ToString();
        _race.text = cardCell.Card.Race.ToString();
        _rarity.text = cardCell.Card.Rarity.ToString();
        _name.text = cardCell.Card.Name;

        _card = cardCell;
    }

    public void ResetCard()
    {
        _card.ResetComponent();
        gameObject.SetActive(false);
    }
}
