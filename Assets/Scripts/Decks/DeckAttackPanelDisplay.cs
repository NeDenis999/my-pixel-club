using System.Collections.Generic;
using Cards.CardCell;
using Data;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Decks
{
    public class DeckAttackPanelDisplay : MonoBehaviour
    {
        [SerializeField] 
        protected List<CardCellMyPage> _cards;

        private DataSaveLoadService _data;
        private AssetProviderService _assetProviderService;
    
        [Inject]
        public void Construct(DataSaveLoadService data, AssetProviderService assetProviderService)
        {
            _data = data;
            _assetProviderService = assetProviderService;
        }

        private void OnEnable()
        {
            UpdateCardDisplay();
        }

        private void UpdateCardDisplay()
        {
            for (int i = 0; i < 5; i++) 
                _cards[i].Render(_data.PlayerData.AttackDecksData[i], _data.PlayerData.AttackDecks[i]);
        }
    }
}