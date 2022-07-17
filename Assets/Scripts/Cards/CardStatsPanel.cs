using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public class CardStatsPanel : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _attackText;

        [SerializeField] 
        private TextMeshProUGUI _defenseText;

        [SerializeField] 
        private TextMeshProUGUI _healthText;
        
        [SerializeField] 
        private TextMeshProUGUI _rarityText;
        
        [SerializeField] 
        private TextMeshProUGUI _levelText;

        public void Init(string attack, string defence, string health, string rarity, string level)
        {
            _attackText.text = attack;
            _defenseText.text = defence;
            _healthText.text = health;
            _rarityText.text = rarity;
            _levelText.text = level;
        }
    }
}