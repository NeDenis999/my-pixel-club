using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Infrastructure.Services
{
    public class DataSaveLoadService
    {
        private const string DataKey = "data";
        private const int EmptyCardId = 0;
        private const int SizeDeck = 5;
        
        protected Sprite[] _avatars;
        protected Card[] _allCards;

        private PlayerData _playerData;
        public PlayerData PlayerData => _playerData;

        public DataSaveLoadService(Card[] allCards, Sprite[] avatars)
        {
            _allCards = allCards;
            /*
            foreach (var card in _allCards)
            {
                card.Repair();
            }
            */
            _avatars = avatars;
        }

        public void Save()
        {
            string jsonString = JsonUtility.ToJson(_playerData);
            PlayerPrefs.SetString(DataKey, jsonString);

            Debug.Log("Save");
        }

        public void Load()
        {
            for (int i = 0; i < _allCards.Length; i++)
            {
                _allCards[i].Id = i;
            }

            if (!PlayerPrefs.HasKey(DataKey))
                CreatePlayerData();
            
            var jsonString = PlayerPrefs.GetString(DataKey);
            
            if (jsonString == "")
                CreatePlayerData();
            
            try
            {
                Debug.Log(jsonString);
                _playerData = JsonUtility.FromJson<PlayerData>(jsonString);
            }
            catch (Exception e)
            {
                CreatePlayerData();

                Debug.LogWarning("Error");
                Debug.LogWarning(e);
            }

            UpdateAttackDeck();
            UpdateDefenceDeck();
            UpdateInventoryDeck();
            UpdateAvatar();

            //Debug.Log(_playerData);
            
            Debug.Log("Load");
            //Debug.Log($"{_playerData.Coins}, \n{_playerData.Crystals}, \n{_playerData.AttackDecks}");
        }

        private void CreatePlayerData()
        {
            var cards = new CardData[5];

            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].Id = EmptyCardId;
                cards[i].Evolution = 1;
                cards[i].Level = 1;
            }

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

            for (int i = 0; i < SizeDeck; i++)
                if (cards[i] != null)
                {
                    _playerData.AttackDecksData[i].Id = cards[i].Id;
                    _playerData.AttackDecksData[i].Evolution = cards[i].Evolution;
                    _playerData.AttackDecksData[i].Level = cards[i].Level;
                    _playerData.AttackDecksData[i].Attack = cards[i].Attack;
                    _playerData.AttackDecksData[i].Defence = cards[i].Def;
                    _playerData.AttackDecksData[i].Health = cards[i].Health;
                    _playerData.AttackDecksData[i].LevelPoint = cards[i].LevelPoint;
                    _playerData.AttackDecksData[i].MaxLevelPoint = cards[i].MaxLevelPoint;
                }
                else
                    _playerData.AttackDecksData[i].Id = 0;

            Save();
        }

        public void SetDefDecks(Card[] cards)
        {
            _playerData.DefDecks = cards;
            
            for (int i = 0; i < SizeDeck; i++)
                if (cards[i] != null)
                {
                    _playerData.DefDecksData[i].Id = cards[i].Id;
                    _playerData.DefDecksData[i].Evolution = cards[i].Evolution;
                    _playerData.DefDecksData[i].Level = cards[i].Level;
                    _playerData.DefDecksData[i].Attack = cards[i].Attack;
                    _playerData.DefDecksData[i].Defence = cards[i].Def;
                    _playerData.DefDecksData[i].Health = cards[i].Health;
                    _playerData.DefDecksData[i].LevelPoint = cards[i].LevelPoint;
                    _playerData.DefDecksData[i].MaxLevelPoint = cards[i].MaxLevelPoint;
                }
                else
                    _playerData.DefDecksData[i].Id = 0;

            Save();
        }

        public void SetInventoryDecks(List<CardCollectionCell> cardsCardCollectionCells)
        {
            var cards = new Card[cardsCardCollectionCells.Count];

            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = Object.Instantiate(cardsCardCollectionCells[i].Card);
                cards[i].Init(
                    cardsCardCollectionCells[i].Evolution, 
                    cardsCardCollectionCells[i].Level, 
                    cardsCardCollectionCells[i].Id, 
                    cardsCardCollectionCells[i].Attack,
                    cardsCardCollectionCells[i].Def,
                    cardsCardCollectionCells[i].Health,
                    cardsCardCollectionCells[i].LevelPoint,
                    cardsCardCollectionCells[i].MaxLevelPoint);
            }
            
            foreach (var card in cards)
            {
                //if (card.LevelPoint > 0)
                    //Debug.Log("Работает 1");
            }

            SetInventoryDecks(cards);
        }

        public void SetInventoryDecks(Card[] cards)
        {
            _playerData.InventoryDecks = cards;
            
            if (_playerData.InventoryDecks.Length != _playerData.InventoryDecksData.Length)
            {
                var inventoryCardsData = new CardData[_playerData.InventoryDecks.Length];

                for (int i = 0; i < _playerData.InventoryDecksData.Length && i < _playerData.InventoryDecks.Length; i++)
                {
                    if (inventoryCardsData[i].Id != EmptyCardId)
                    {
                        inventoryCardsData[i] = _playerData.InventoryDecksData[i];
                    }
                }
                
                _playerData.InventoryDecksData = inventoryCardsData;
            }
            
            for (int i = 0; i < _playerData.InventoryDecks.Length; i++)
            {
                _playerData.InventoryDecksData[i].Id = cards[i].Id;
                _playerData.InventoryDecksData[i].Evolution = cards[i].Evolution;
                _playerData.InventoryDecksData[i].Level = cards[i].Level;
                _playerData.InventoryDecksData[i].Attack = cards[i].Attack;
                _playerData.InventoryDecksData[i].Defence = cards[i].Def;
                _playerData.InventoryDecksData[i].Health = cards[i].Health;
                _playerData.InventoryDecksData[i].LevelPoint = cards[i].LevelPoint;
                _playerData.InventoryDecksData[i].MaxLevelPoint = cards[i].MaxLevelPoint;
                
                //if (_playerData.InventoryDecksData[i].LevelPoint > 0)
                    //Debug.Log("Работает 2");
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
            if (_playerData.InventoryDecksData == null)
            {
                _playerData.InventoryDecksData = new CardData[0];
            }
            else
            {
                var inventoryDecks = new Card[_playerData.InventoryDecksData.Length];

                for (int i = 0; i < _playerData.InventoryDecksData.Length; i++)
                {
                    var currentCard = _playerData.InventoryDecksData[i];
                    
                    inventoryDecks[i] = Object.Instantiate(_allCards[_playerData.InventoryDecksData[i].Id]);
                    inventoryDecks[i].Init(
                        currentCard.Evolution,
                        currentCard.Level,
                        currentCard.Id,
                        currentCard.Attack,
                        currentCard.Defence,
                        currentCard.Health,
                        currentCard.LevelPoint,
                        currentCard.MaxLevelPoint);
                    
                    if (inventoryDecks[i].LevelPoint > 0)
                        Debug.Log($"Работает 3 {inventoryDecks[i].LevelPoint} {inventoryDecks[i].MaxLevelPoint}");
                }
                
                _playerData.InventoryDecks = inventoryDecks;
            }
        }

        private void UpdateDefenceDeck()
        {
            var defenceDecks = new Card[5];

            if (_playerData.DefDecksData == null)
            {
                for (int i = 0; i < defenceDecks.Length; i++) 
                    defenceDecks[i] = _allCards[EmptyCardId];
            }
            else
            {
                for (int i = 0; i < _playerData.DefDecksData.Length; i++)
                {
                    var currentCard = _playerData.DefDecksData[i];
                    
                    defenceDecks[i] = Object.Instantiate(_allCards[_playerData.DefDecksData[i].Id]);
                    defenceDecks[i].Init(
                        currentCard.Evolution,
                        currentCard.Level,
                        currentCard.Id,
                        currentCard.Attack,
                        currentCard.Defence,
                        currentCard.Health,
                        currentCard.LevelPoint,
                        currentCard.MaxLevelPoint);
                }
            }

            _playerData.DefDecks = defenceDecks;
        }

        private void UpdateAttackDeck()
        {
            var attackDecks = new Card[5];

            if (_playerData.AttackDecksData == null)
            {
                for (int i = 0; i < attackDecks.Length; i++) 
                    attackDecks[i] = _allCards[EmptyCardId];
            }
            else
            {
                for (int i = 0; i < _playerData.AttackDecksData.Length; i++)
                {
                    var currentCard = _playerData.AttackDecksData[i];
                    
                    attackDecks[i] = Object.Instantiate(_allCards[_playerData.AttackDecksData[i].Id]);
                    attackDecks[i].Init(
                        currentCard.Evolution,
                        currentCard.Level,
                        currentCard.Id,
                        currentCard.Attack,
                        currentCard.Defence,
                        currentCard.Health,
                        currentCard.LevelPoint,
                        currentCard.MaxLevelPoint);
                }
            }
            
            _playerData.AttackDecks = attackDecks;
        }
    }
}