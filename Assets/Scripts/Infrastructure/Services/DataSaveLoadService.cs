using System;
using Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Infrastructure.Services
{
    public class DataSaveLoadService
    {
        private const string DataKey = "data";
        public PlayerData PlayerData => _playerData;
        
        private Card _emptyCard;
        private PlayerData _playerData;
        private Sprite[] _avatars;
        
        public DataSaveLoadService(Card emptyCard, Sprite[] avatars)
        {
            _emptyCard = emptyCard;
            _avatars = avatars;
        }

        public void Save()
        {
            string jsonString = JsonUtility.ToJson(_playerData);
            PlayerPrefs.SetString(DataKey, jsonString);

            string info = "";

            if (_playerData.AttackDecks != null)
            {
                foreach (var attackDeck in _playerData.AttackDecks)
                {
                    if (attackDeck)
                        info += $"{attackDeck.Name}\n";
                    else
                        info += "NONE\n";
                }
            }

            Debug.Log("Save");
            Debug.Log($"{_playerData.Coins}, \n{_playerData.Crystals}, \n{_playerData.AttackDecks} \n{info}");
        }

        public void Load()
        {
            var jsonString = PlayerPrefs.GetString(DataKey);

            //if (jsonString != "")
            //{
                try
                {
                    _playerData = JsonUtility.FromJson<PlayerData>(jsonString);
                    
                    for (int i = 0; i < _playerData.AttackDecks.Length; i++) 
                        if (!_playerData.AttackDecks[i])
                            _playerData.AttackDecks[i] = _emptyCard;
                
                    for (int i = 0; i < _playerData.DefDecks.Length; i++) 
                        if (!_playerData.DefDecks[i])
                            _playerData.DefDecks[i] = _emptyCard;
                }
                catch (Exception e)
                {
                    _playerData = new PlayerData();
                    _playerData.Coins = 1000;
                    _playerData.Crystals = 1000;

                    var cards = new Card[5];

                    for (int i = 0; i < cards.Length; i++) 
                        cards[i] = _emptyCard;
                
                    _playerData.AttackDecks = cards;;
                    _playerData.DefDecks = cards;
                    _playerData.Nickname = RandomNickname();
                    _playerData.Avatar = RandomAvatar();
                    _playerData.FirstDayInGame = DateTime.Now;
                    _playerData.Rank = 1;
                    _playerData.Level = 1;
                    _playerData.Energy = 25;

                    Save();
                    
                    Debug.LogWarning("All Save Update");
                    Debug.LogWarning(e);
                }
            //}

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
            Save();
        }
        
        public void SetDefDecks(Card[] cards)
        {
            _playerData.DefDecks = cards;
            Save();
        }

        public void SetInventoryDecks(Card[] cards)
        {
            _playerData.InventoryDecks = cards;
            Save();
        }
        
        private string RandomNickname()
        {
            var nickNames = new[]
                { "Tijagi", "Luxulo", "Lofuwa", "Xyboda", "Sopogy", "Lydiba", "Dekale", "Tareqi", "Muqawo", "Dejalo" };

            return nickNames[Random.Range(0, nickNames.Length)];
        }
        
        private Sprite RandomAvatar() =>
            _avatars[Random.Range(0, _avatars.Length)];
    }
}