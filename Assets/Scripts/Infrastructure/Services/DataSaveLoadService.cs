using System;
using Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Infrastructure.Services
{
    public class DataSaveLoadService
    {
        private const string DataKey = "data";
        private const int EmptyCardId = 0;
        
        protected Sprite[] _avatars;
        protected Card[] _allCards;

        private PlayerData _playerData;
        
        public PlayerData PlayerData => _playerData;

        public DataSaveLoadService(Card[] allCards, Sprite[] avatars)
        {
            _allCards = allCards;
            _avatars = avatars;
        }

        public void Save()
        {
            string jsonString = JsonUtility.ToJson(_playerData);
            PlayerPrefs.SetString(DataKey, jsonString);

            Debug.Log("Save");
            Debug.Log($"{_playerData.Coins}, \n{_playerData.Crystals}, \n{_playerData.AttackDecks}");
        }

        public void Load()
        {
            for (int i = 0; i < _allCards.Length; i++) 
                _allCards[i].Id = i;

            var jsonString = PlayerPrefs.GetString(DataKey);
            
            try
            {
                _playerData = JsonUtility.FromJson<PlayerData>(jsonString);
            }
            catch (Exception e)
            {
                var cards = new CardData[5];

                for (int i = 0; i < cards.Length; i++) 
                    cards[i].Id = EmptyCardId;
                
                _playerData = new PlayerData
                {
                    Coins = 1000,
                    Crystals = 1000,
                    AttackDecksData = cards,
                    DefDecksData = cards,
                    InventoryDecksData = new CardData[0],
                    InventoryDecks = new Card[0],
                    Nickname = RandomNickname(),
                    AvatarId = RandomAvatarId(),
                    FirstDayInGame = DateTime.Now,
                    Rank = 1,
                    Level = 1,
                    Energy = 25
                };

                Save();
                    
                Debug.LogWarning("All Save Update");
                Debug.LogWarning(e);
            }
            
            UpdateAttackDeck();
            UpdateDefenceDeck();
            UpdateInventoryDeck();
            UpdateAvatar();

            Debug.Log("Load");
            Debug.Log($"{_playerData.Coins}, \n{_playerData.Crystals}, \n{_playerData.AttackDecks}");
        }

        public void SetCoinCount(int count)
        {
            _playerData.Coins = count;
            Save();
        }
        
        public void SetCrystalsCount(int count)
        {
            _playerData.Crystals = count;
            Save();
        }

        public void SetAttackDecks(Card[] cards)
        {
            _playerData.AttackDecks = cards;

            for (int i = 0; i < cards.Length; i++) 
                _playerData.AttackDecksData[i].Id = cards[i].Id;

            Save();
        }
        
        public void SetDefDecks(Card[] cards)
        {
            _playerData.DefDecks = cards;
            
            for (int i = 0; i < cards.Length; i++) 
                _playerData.DefDecksData[i].Id = cards[i].Id;

            Save();
        }

        public void SetInventoryDecks(Card[] cards)
        {
            _playerData.InventoryDecks = cards;
            
            Debug.Log(_playerData.InventoryDecks.Length);
            Debug.Log(_playerData.InventoryDecksData.Length);

            if (_playerData.InventoryDecks.Length != _playerData.InventoryDecksData.Length)
            {
                var inventoryCardsData = new CardData[_playerData.InventoryDecks.Length];

                for (int i = 0; i < _playerData.InventoryDecksData.Length && i < _playerData.InventoryDecks.Length; i++)
                {
                    if (inventoryCardsData[i].Id != EmptyCardId)
                        inventoryCardsData[i] = _playerData.InventoryDecksData[i];
                }

                Debug.Log(inventoryCardsData.Length);
                _playerData.InventoryDecksData = inventoryCardsData;
            }
            
            for (int i = 0; i < _playerData.InventoryDecks.Length; i++)
            {
                _playerData.InventoryDecksData[i].Id = _playerData.InventoryDecks[i].Id;
            }
            
            Save();
        }
        
        private string RandomNickname()
        {
            var nickNames = new[]
                { "Tijagi", "Luxulo", "Lofuwa", "Xyboda", "Sopogy", "Lydiba", "Dekale", "Tareqi", "Muqawo", "Dejalo" };

            return nickNames[Random.Range(0, nickNames.Length)];
        }
        
        private int RandomAvatarId() =>
            Random.Range(0, _avatars.Length);
        
        private void UpdateAvatar() => 
            _playerData.Avatar = _avatars[_playerData.AvatarId];

        private void UpdateInventoryDeck()
        {
            var inventoryDecks = new Card[_playerData.InventoryDecksData.Length];

            for (int i = 0; i < _playerData.InventoryDecksData.Length; i++)
            {
                inventoryDecks[i] = _allCards[_playerData.InventoryDecksData[i].Id];
            }

            _playerData.InventoryDecks = inventoryDecks;
        }

        private void UpdateDefenceDeck()
        {
            var defenceDecks = new Card[5];

            for (int i = 0; i < _playerData.DefDecksData.Length; i++)
            {
                defenceDecks[i] = _allCards[_playerData.DefDecksData[i].Id];
            }

            _playerData.DefDecks = defenceDecks;
        }

        private void UpdateAttackDeck()
        {
            var attackDecks = new Card[5];

            for (int i = 0; i < _playerData.AttackDecksData.Length; i++)
            {
                attackDecks[i] = _allCards[_playerData.AttackDecksData[i].Id];
            }

            _playerData.AttackDecks = attackDecks;
        }
    }
}