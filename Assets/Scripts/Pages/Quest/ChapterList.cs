using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FarmPage.Quest
{
    public class ChapterList : MonoBehaviour
    {
        [SerializeField] 
        private Chapter[] _chapters;

        [SerializeField] private Scrollbar _srollbar;

        private DataSaveLoadService _dataSaveLoadService;
        
        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService)
        {
            _dataSaveLoadService = dataSaveLoadService;
        }

        public void InitAllChapter()
        {
            _srollbar.value = 1;

            for (int i = 0; i < _chapters.Length; i++)
            {
                if (_dataSaveLoadService.PlayerData.CountQuestPassed >= i)
                    _chapters[i].UnlockedChapter();

                if(_dataSaveLoadService.PlayerData.CountQuestPassed > i)
                    _srollbar.value -= 0.15f;

                _chapters[i].Id = i;
            }
        }
        
        public void CloseAllChapters()
        {
            foreach (var chapter in _chapters) 
                chapter.Close();
        }
    }
}