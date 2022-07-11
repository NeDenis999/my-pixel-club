using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Pages.Quest
{
    public class ChapterList : MonoBehaviour
    {
        [SerializeField] 
        private Chapter[] _chapters;

        private DataSaveLoadService _dataSaveLoadService;
        
        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService)
        {
            _dataSaveLoadService = dataSaveLoadService;
        }

        public void InitAllChapter()
        {
            for (int i = 0; i < _chapters.Length; i++)
            {
                if (_dataSaveLoadService.PlayerData.CountQuestPassed >= i)
                    _chapters[i].UnlockedChapter();

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