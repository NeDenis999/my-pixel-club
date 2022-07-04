using System.Collections;
using System.Globalization;
using Data;
using DG.Tweening;
using Infrastructure.Services;
using TMPro;
using UnityEditor.U2D.Path.GUIFramework;
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
        private SliderAnimator _energySlider;
    
        [SerializeField]
        private SliderAnimator _xpSlider;

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
            _xpSlider.UpdateSlider(_data.PlayerData.XP);
            
            _avatar.sprite = _data.PlayerData.Avatar;
            _nickName.text = _data.PlayerData.Nickname;
            _levelText.text = _data.PlayerData.Level.ToString();
            _rankText.text = _data.PlayerData.Rank.ToString();
            _energyText.text = _data.PlayerData.Energy.ToString(CultureInfo.InvariantCulture);
            _xpText.text = _player.Exp.ToString(CultureInfo.InvariantCulture);
            _heroesText.text = (CalculateHerouseCountInDeck(_data.PlayerData.AttackDecks) + CalculateHerouseCountInDeck(_data.PlayerData.DefDecks) + _data.PlayerData.InventoryDecks.Length).ToString() + '/' + 100;
            _powerText.text = 90.ToString();
            _goldText.text = _data.PlayerData.Coins.ToString();
        }

        private int CalculateHerouseCountInDeck(Card[] deck)
        {
            int herouseCount = 0;

            foreach (var cardInDeck in deck)
            {
                if (cardInDeck.Rarity != RarityCard.Epmpty)
                    herouseCount++;
            }

            return herouseCount;
        }
    }
}
