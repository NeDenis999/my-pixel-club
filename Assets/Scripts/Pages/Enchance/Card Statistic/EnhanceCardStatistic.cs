using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnhanceCardStatistic : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _atk, _def, _rarity, _race, _name, _health, _level;    

    public void Render(CardCell cardForDelete)
    {
        _icon.sprite = cardForDelete.Card.UIIcon;

        _atk.text = "ATK: " + cardForDelete.Attack;
        _def.text = "DEF: " + cardForDelete.Def;
        _health.text = "HP: " + cardForDelete.Card.Health;
        _level.text = "Level: " + cardForDelete.Level;
        _race.text = cardForDelete.Card.Race.ToString();
        _rarity.text = cardForDelete.Card.Rarity.ToString();
        _name.text = cardForDelete.Card.Name;
    }
}
