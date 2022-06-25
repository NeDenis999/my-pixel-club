using UnityEngine;

namespace Data
{
    public class DataSaveLoadService
    {
        private const string DataKey = "data";
        public PlayerData PlayerData => _playerData;
        
        private Card _emptyCard;
        
        private PlayerData _playerData;

        public DataSaveLoadService(Card emptyCard)
        {
            _emptyCard = emptyCard;
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
            
            if(jsonString != "")
                _playerData = JsonUtility.FromJson<PlayerData>(jsonString);
            else
            {
                _playerData = new PlayerData();
                SetCoinCount(1000);
                SetCrystalsCount(1000);

                var cards = new Card[5];

                for (int i = 0; i < cards.Length; i++) 
                    cards[i] = _emptyCard;
                
                SetAttackDecks(cards);
                SetDefDecks(cards);
            }
            
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
    }
}