using System.Collections.Generic;
using Cards.Deck.CardCell;
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
    
        [Inject]
        public void Construct(DataSaveLoadService data)
        {
            _data = data;
            UpdateCardDisplay();
        }

        private void OnEnable()
        {
            UpdateCardDisplay();
        }

        private void UpdateCardDisplay()
        {
            for (int i = 0; i < 5; i++) 
                _cards[i].Render(_data.PlayerData.AttackDecks[i]);
        }
    }
}