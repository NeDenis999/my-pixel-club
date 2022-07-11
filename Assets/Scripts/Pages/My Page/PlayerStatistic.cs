using System.Collections;
using System.Globalization;
using Collection;
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
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _rankText;
        [SerializeField] private TMP_Text _energyText;
        [SerializeField] private TMP_Text _expText;
        [SerializeField] private TMP_Text _heroesText;
        [SerializeField] private TMP_Text _powerText;
        [SerializeField] private TMP_Text _goldText;
        [SerializeField] private TMP_Text _nickName;
        [SerializeField] private Image _avatar;

        [SerializeField]
        private SliderAnimator _energySlider;
    
        [SerializeField]
        private SliderAnimator _expSlider;

        [SerializeField] private AttackDeck _attackDeck;

        private DataSaveLoadService _data;
        
        [Inject]
        private void Construct(DataSaveLoadService data)
        {
            _data = data;
        }        

        private void OnEnable()
        {
            UpdateDisplay();            
        }

        public void UpdateDisplay()
        {
            _energySlider.UpdateSlider(_data.PlayerData.Energy);
            _expSlider.UpdateSlider(_data.PlayerData.EXP, _data.PlayerData.MaxExp);
            
            _avatar.sprite = _data.PlayerData.Avatar;
            _nickName.text = _data.PlayerData.Nickname;
            _levelText.text = _data.PlayerData.Level.ToString();
            _rankText.text = _data.PlayerData.Rank.ToString();
            _energyText.text = _data.PlayerData.Energy.ToString(CultureInfo.InvariantCulture);
            _expText.text = _data.PlayerData.EXP.ToString(CultureInfo.InvariantCulture);
            _heroesText.text = _data.AmountCards.ToString() + '/' + 50;
            _powerText.text = _attackDeck.Power.ToString();
            _goldText.text = _data.PlayerData.Coins.ToString();
        }
    }
}
