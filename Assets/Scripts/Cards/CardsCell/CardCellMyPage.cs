using System;
using Data;
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
        private Image _skillIcon;
        
        [SerializeField] 
        private TextMeshProUGUI _attackText;

        [SerializeField] 
        private TextMeshProUGUI _defenseText;

        [SerializeField] 
        private TextMeshProUGUI _healthText;

        [SerializeField] 
        private GameObject _statsPanel;

        [SerializeField] private Sprite _defaultIcon;
        private Vector2 _startPosition = Vector2.zero;

        public void Render(CardData cardData, global::Card card)
        {
            if (cardData.Id != 0)
            {
                _icon.sprite = card.UIIcon;
                _icon.transform.localPosition = _startPosition + card.DirectionView;
                
                _statsPanel.SetActive(true);
                _attackText.text = cardData.Attack.ToString();
                _defenseText.text = cardData.Defence.ToString();
                _healthText.text = cardData.Health.ToString();
                _skillIcon.sprite = card.SkillIcon;
            }
            else
            {
                _icon.sprite = _defaultIcon;
                _statsPanel.SetActive(false);
            }
        }
    }
}