using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pages.My_Page
{
    public class PlayerStatistic : MonoBehaviour
    {
        [SerializeField] private Player _player;

        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _rankText;
        [SerializeField] private TMP_Text _energyText;
        [SerializeField] private TMP_Text _xpText;
        [SerializeField] private TMP_Text _heroesText;
        [SerializeField] private TMP_Text _powerText;
        [SerializeField] private TMP_Text _goldText;

        [SerializeField]
        private Slider _energySlider;
    
        [SerializeField]
        private Slider _xpSlider;

        private void Start()
        {
            _player.OnLevelChange += level => _levelText.text = level.ToString() + "/100";
        
            _player.OnEnergyChange += energy =>
            {
                _energyText.text = energy + "/25";
                _energySlider.value = energy;
            };
        }
    }
}
