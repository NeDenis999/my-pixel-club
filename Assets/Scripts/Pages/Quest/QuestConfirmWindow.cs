using Infrastructure.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace Pages.Quest
{
    public class QuestConfirmWindow : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private int _requiredAmountEnergy;
        [SerializeField] private GameObject _questList, _quest, _exeptionBaner;
        [SerializeField] private TMP_Text _exeptionBanerText;

        private DataSaveLoadService _dataSaveLoadService;
    
        public int RequiredAmountEnergy => _requiredAmountEnergy;

        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService)
        {
            _dataSaveLoadService = dataSaveLoadService;
        }
    
        public void StartQuest()
        {
            if (!CheckForPlayerAlive() || !CheckForDeckEmpty() || !CheckForEnergy())
                return;
            
            _quest.SetActive(true);
            _questList.SetActive(false);
            gameObject.SetActive(false);
        }

        private bool CheckForEnergy()
        {
            print(_player.Energy);
        
            if (_requiredAmountEnergy <= _player.Energy)
                return true;

            OpenExceptionBanner("Not enough energy");
            return false;
        }

        private bool CheckForPlayerAlive()
        {
            print(_player.Health);
        
            if (_player.Health > 0)
                return true;
        
            OpenExceptionBanner("Not enough health");
            return false;
        }

        private bool CheckForDeckEmpty()
        {
            foreach (var card in _dataSaveLoadService.PlayerData.AttackDecks)
            {
                if (card.Id != 0)
                    return true;
            }

            OpenExceptionBanner("Not card in deck");
            return false;
        }

        private void OpenExceptionBanner(string exceptionName)
        {
            gameObject.SetActive(false);
            _exeptionBaner.SetActive(true);
            _exeptionBanerText.text = exceptionName;
        }
    }
}
