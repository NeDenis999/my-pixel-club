using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cards.CardCell
{
    public class CardCellMyPage : MonoBehaviour
    {
        [SerializeField] 
        private Image _icon;
        
        [SerializeField] 
        private TextMeshProUGUI _attackText;

        [SerializeField] 
        private TextMeshProUGUI _defenseText;

        [SerializeField] 
        private TextMeshProUGUI _healthText;

        [SerializeField] 
        private GameObject _statsPanel;
        
        public void Render(global::Card card)
        {
            if (card.Attack != 0 && card.Def != 0)
            {
                _icon.sprite = card.UIIcon;
                _icon.transform.localPosition = _icon.transform.localPosition.ToMove(card.DirectionView);
                
                _statsPanel.SetActive(true);
                _attackText.text = card.Attack.ToString();
                _defenseText.text = card.Def.ToString();
                _healthText.text = card.Health.ToString();
            }
            else
            {
                _statsPanel.SetActive(false);
            }
        }
    }
}