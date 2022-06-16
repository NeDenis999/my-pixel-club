using UnityEngine;

namespace Data
{
    public class DataSaveLoadService
    {
        private const string DataKey = "data";
        public PlayerData PlayerData => _playerData;

        private PlayerData _playerData;
        
        public void Save()
        {
            string jsonString = JsonUtility.ToJson(_playerData);
            PlayerPrefs.SetString(DataKey, jsonString);
            
            Debug.Log("Save");
        }

        public void Load()
        {
            var jsonString = PlayerPrefs.GetString(DataKey);
            _playerData = JsonUtility.FromJson<PlayerData>(jsonString);
            
            Debug.Log("Load");
        }

        public void SetCoinCount(int count)
        {
            _playerData.Coins = count;
        }
        
        public void SetCrystalsCount(int count)
        {
            _playerData.Crystals = count;
        }

        public void SetAttackDecks(Card[] cards)
        {
            _playerData.AttackDecks = cards;
        }
        
        public void SetDefDecks(Card[] cards)
        {
            _playerData.DefDecks = cards;
        }
    }
}