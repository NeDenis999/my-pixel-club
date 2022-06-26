using Data;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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
        [SerializeField] private TMP_Text _nickName;
        [SerializeField] private Image _avatar;

        [SerializeField]
        private Slider _energySlider;
    
        [SerializeField]
        private Slider _xpSlider;

        private DataSaveLoadService _data;
        
        [Inject]
        private void Construct(DataSaveLoadService data)
        {
            _data = data;
        }
        
        private void Start()
        {
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            var energy = _energySlider.value;
            var xp = _xpSlider.value;

            _energySlider.value = 0;
            _xpSlider.value = 0;
            
            DOTween.To(()=> _energySlider.value, x=> _energySlider.value = x, energy, 1); 
            DOTween.To(()=> _xpSlider.value, x=> _xpSlider.value = x, xp, 1); 
            
            _avatar.sprite = _data.PlayerData.Avatar;
            _nickName.text = _data.PlayerData.Nickname;
            _levelText.text = 1.ToString();
            _rankText.text = 1500.ToString();
            _energyText.text = _player.Energy.ToString();
            _xpText.text = _player.Exp.ToString();
            _xpSlider.value = _player.Exp;
            _heroesText.text = 5.ToString() + '/' + 25;
            _powerText.text = 90.ToString();
            _goldText.text = _data.PlayerData.Coins.ToString();
        }
    }
}
