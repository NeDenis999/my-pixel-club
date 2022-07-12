using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public class CardStatsPanel : MonoBehaviour
    {
        [SerializeField] 
        private Image _skillIcon;
        
        [SerializeField] 
        private TextMeshProUGUI _attackText;

        [SerializeField] 
        private TextMeshProUGUI _defenseText;

        [SerializeField] 
        private TextMeshProUGUI _healthText;

        public void Init(string attack, string defence, string health, Sprite skillIcon)
        {
            _attackText.text = attack;
            _defenseText.text = defence;
            _healthText.text = health;
            _skillIcon.sprite = skillIcon;
        }
    }
}