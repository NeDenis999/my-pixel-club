using System;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace FarmPage.Battle
{
    public class PlayerStatisticBattle : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _levelText;
        
        [SerializeField] 
        private TextMeshProUGUI _energyText;
        
        [SerializeField] 
        private TextMeshProUGUI _defenceText;
        
        [SerializeField] 
        private TextMeshProUGUI _attackText;

        private LocalDataService _localDataService;
        private DataSaveLoadService _dataSaveLoadService;
        
        [Inject]
        private void Construct(LocalDataService localDataService, DataSaveLoadService dataSaveLoadService)
        {
            _localDataService = localDataService;
            _dataSaveLoadService = dataSaveLoadService;
        }
        
        private void OnEnable()
        {
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            _levelText.text = $"LEVEL: {_dataSaveLoadService.PlayerData.Level}";
            _energyText.text = $"ENERGY: {_dataSaveLoadService.PlayerData.Energy}/{25}";
            _defenceText.text = $"DEFENCE: {_localDataService.Defence}";
            _attackText.text = $"ATTACK: {_localDataService.Attack}";
        }
    }
}