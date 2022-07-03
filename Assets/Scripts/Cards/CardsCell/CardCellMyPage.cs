using System;
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

        private Sprite _defaultIcon;
        private Vector2 _startPosition;
        
        private void Awake()
        {
            _startPosition = _icon.transform.localPosition;
            _defaultIcon = _icon.sprite;
        }

        public void Render(global::Card card)
        {
            if (card.Name != "Empty")
            {
                _icon.sprite = card.UIIcon;
                _icon.transform.localPosition = _startPosition + card.DirectionView;
                
                _statsPanel.SetActive(true);
                _attackText.text = card.Attack.ToString();
                _defenseText.text = card.Def.ToString();
                _healthText.text = card.Health.ToString();
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