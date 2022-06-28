using System.Collections;
using System.Globalization;
using Data;
using DG.Tweening;
using Infrastructure.Services;
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
            
            //_avatar.sprite = _data.PlayerData.Avatar;
        }

        public void UpdateDisplay()
        {
            UpdateSlider(_energySlider, _data.PlayerData.Energy);
            UpdateSlider(_xpSlider, _data.PlayerData.XP);
            
            _nickName.text = _data.PlayerData.Nickname;
            _levelText.text = _data.PlayerData.Level.ToString();
            _rankText.text = _data.PlayerData.Rank.ToString();
            _energyText.text = _data.PlayerData.Energy.ToString(CultureInfo.InvariantCulture);
            _xpText.text = _player.Exp.ToString(CultureInfo.InvariantCulture);
            _xpSlider.value = _player.Exp;
            _heroesText.text = 5.ToString() + '/' + 25;
            _powerText.text = 90.ToString();
            _goldText.text = _data.PlayerData.Coins.ToString();
        }

        private void UpdateSlider(Slider slider, float value)
        {
            slider.value = 0;
            DOTween.To(()=> slider.value, x=> slider.value = x, value, 1); 
        }
    }
}
