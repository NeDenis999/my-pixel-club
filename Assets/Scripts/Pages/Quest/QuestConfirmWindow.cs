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
        [SerializeField] private GameObject _questList, _exeptionBaner;
        [SerializeField] private TMP_Text _exeptionBanerText;
        [SerializeField] private QuestFight _questFight;

        private DataSaveLoadService _dataSaveLoadService;
    
        public int RequiredAmountEnergy => _requiredAmountEnergy;

        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService)
        {
            _dataSaveLoadService = dataSaveLoadService;
        }
    
        public void StartQuest()
        {
            if (_requiredAmountEnergy > _dataSaveLoadService.PlayerData.Energy)
            {
                OpenExceptionBanner("Not enough energy");
                return;
            }

            if (CheckForDeckEmpty() == false)
                return;

            _questFight.StartFight();
            _questList.SetActive(false);
            gameObject.SetActive(false);
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
