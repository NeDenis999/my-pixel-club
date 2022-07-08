using System;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Pages.Battle
{
    public class PlayerAvatar : MonoBehaviour
    {
        [SerializeField] 
        private Image _icon;

        private DataSaveLoadService _dataSaveLoadService;
        
        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService)
        {
            _dataSaveLoadService = dataSaveLoadService;
        }
        
        private void Start()
        {
            _icon.sprite = _dataSaveLoadService.PlayerData.Avatar;
        }
    }
}