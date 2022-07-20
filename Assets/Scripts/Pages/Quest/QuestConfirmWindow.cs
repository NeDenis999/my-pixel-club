using Infrastructure.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace FarmPage.Quest
{
    public class QuestConfirmWindow : MonoBehaviour
    {
        [SerializeField] private int _requiredAmountEnergy;
        [SerializeField] private GameObject _questList, _exeptionBaner;
        [SerializeField] private TMP_Text _exeptionBanerText;
        [SerializeField] private QuestFight _questFight;
        [SerializeField] private UpPanel _upPanel;
        
        private DataSaveLoadService _dataSaveLoadService;
        private LocalDataService _localDataService;
        
        public int RequiredAmountEnergy => _requiredAmountEnergy;

        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService, LocalDataService localDataService)
        {
            _dataSaveLoadService = dataSaveLoadService;
            _localDataService = localDataService;
        }
    
        public void StartQuest(Chapter chapter)
        {
            if (_requiredAmountEnergy > _dataSaveLoadService.PlayerData.Energy)
            {
                OpenExceptionBanner("Not enough energy");
                return;
            }

            if (CheckForDeckEmpty() == false)
                return;

            _questFight.StartFight(chapter);
            _upPanel.Block();
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
