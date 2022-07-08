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

        private readonly Sprite[] _avatars;
        private readonly Card[] _allCards;

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
        }

        public void Load()
        {
            for (int i = 0; i < _allCards.Length; i++) 
                _allCards[i].Id = i;

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
        }
        
        public void IncreaseEnergy(int energyValue)
        {
            if (_playerData.Energy > 25) throw new ArgumentOutOfRangeException();

            _playerData.Energy += energyValue;
            Save();
        }

        public void DecreaseEnergy(int energyValue)
        {
            if (energyValue > _playerData.Energy) throw new ArgumentOutOfRangeException();

            _playerData.Energy -= energyValue;
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

        public void SetInventoryDecks(List<CardCollectionCell> cardsCardCollectionCells)
        {
            var cards = new CardData[cardsCardCollectionCells.Count];

            for (int i = 0; i < cards.Length; i++) 
                cards[i] = cardsCardCollectionCells[i].CardData;

            SetInventoryDecks(cards);
        }

        public void SetInventoryDecks(CardData[] cards) => 
            SetDecks(cards, ref _playerData.InventoryDecksData, ref _playerData.InventoryDecks);
        
        public void SetDefDecks(CardData[] cards) => 
            SetDecks(cards, ref _playerData.DefDecksData, ref _playerData.DefDecks);

        public void SetAttackDecks(CardData[] cards) => 
            SetDecks(cards, ref _playerData.AttackDecksData, ref _playerData.AttackDecks);

        private void SetDecks(CardData[] cards, ref CardData[] deckData, ref Card[] deck)
        {
            deck = new Card[cards.Length];
            deckData = cards;

            for (int i = 0; i < cards.Length; i++)
            {
                var currentCardData = cards[i];
                var currentCard = Object.Instantiate(_allCards[currentCardData.Id]);
                
                currentCard.Init(
                    currentCardData.Evolution,
                    currentCardData.Level,
                    currentCardData.Id,
                    currentCardData.Attack,
                    currentCardData.Defence,
                    currentCardData.Health,
                    currentCardData.LevelPoint,
                    currentCardData.MaxLevelPoint);

                deck[i] = currentCard;
            }

            Save();
        }
        
        private void CreatePlayerData()
        {
            _playerData = new PlayerData
            {
                Coins = 1000,
                Crystals = 1000,
                AttackDecksData = CreateCardsData(),
                DefDecksData = CreateCardsData(),
                InventoryDecksData = Array.Empty<CardData>(),
                InventoryDecks = Array.Empty<Card>(),
                Nickname = RandomNickname(),
                AvatarId = RandomAvatarId(),
                FirstDayInGame = DateTime.Now,
                Rank = 1,
                Level = 1,
                Energy = 25
            };

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

        private void UpdateInventoryDeck() => 
            UpdateDeck(ref _playerData.InventoryDecksData, ref _playerData.InventoryDecks);

        private void UpdateDefenceDeck() => 
            UpdateDeck(ref _playerData.DefDecksData, ref _playerData.DefDecks);

        private void UpdateAttackDeck() => 
            UpdateDeck(ref _playerData.AttackDecksData, ref _playerData.AttackDecks);

        private void UpdateDeck(ref CardData[] deckData, ref Card[] decks)
        {
            decks = new Card[deckData.Length];

            if (deckData == null)
            {
                for (int i = 0; i < decks.Length; i++) 
                    decks[i] = _allCards[EmptyCardId];
            }
            else
            {
                for (int i = 0; i < deckData.Length; i++)
                {
                    var currentCard = deckData[i];
                    
                    decks[i] = Object.Instantiate(_allCards[deckData[i].Id]);
                    decks[i].Init(
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
        }

        private CardData[] CreateCardsData()
        {
            var cards = new CardData[5];
            
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].Id = EmptyCardId;
                cards[i].Evolution = 1;
                cards[i].Level = 1;
                cards[i].MaxLevelPoint = 1000;
            }

            return cards;
        }
    }
}