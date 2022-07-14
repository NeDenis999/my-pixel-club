using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatisticWindow : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _frame;
    [SerializeField] private TMP_Text _atk, _def, _rarity, _race, _name, _health;
    [SerializeField] private CardCollection _cardCollection;
    [SerializeField] private Sprite _emptyCardSprite;
    [SerializeField] private Button _resetButton;

    private CardCellInDeck _card; 

    private void OnEnable()
    {
        _cardCollection.gameObject.SetActive(false);
        ClearStatisticFields();
        _resetButton.gameObject.SetActive(false);
    }

    private void ClearStatisticFields()
    {
        _icon.sprite = _emptyCardSprite;
        _frame.gameObject.SetActive(false);

        _atk.text = "Atk: 0";
        _def.text = "Def: 0";
        _health.text = "HP: 0";
        _race.text = "Race: ";
        _rarity.text = "Rare: ";
        _name.text = "Name: ";
    }

    public void Render(CardCellInDeck cardCell)
    {
        _icon.sprite = cardCell.Icon.sprite;
        
        if (cardCell.CardData.Id != 0)
        {
            _frame.sprite = cardCell.Card.GetFrame();
            _frame.gameObject.SetActive(true);
        }
        else
            _frame.gameObject.SetActive(false);

        _atk.text = "Atk: " + cardCell.Attack;
        _def.text = "Def: " + cardCell.Def;
        _health.text = "HP: " + cardCell.Health.ToString();
        _race.text = cardCell.Card.Race.ToString();
        _rarity.text = cardCell.Card.Rarity.ToString();
        _name.text = cardCell.Card.Name;

        _card = cardCell;

        _resetButton.gameObject.SetActive(true);
    }

    public void ResetCard()
    {
        _card.ResetComponent();
        ClearStatisticFields();
        _resetButton.gameObject.SetActive(false);
    }
}
