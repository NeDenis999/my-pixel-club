using UnityEngine;
using UnityEngine.UI;

namespace Pages.Quest
{
    public class Chapter : MonoBehaviour
    {
        private const float OpenSize = 471.36f;
        private const float CloseSize = 100f;

        [SerializeField]
        private RectTransform _rectTransform;

        [SerializeField]
        private GameObject _info;

        [SerializeField]
        private VerticalLayoutGroup _verticalLayoutGroup;

        [SerializeField]
        private ChapterList _chapterList;

        [SerializeField] 
        private Chapter _nextChapter;

        [SerializeField] 
        private EnemyQuestData[] _enemyQuestsData;
        public Chapter NextChapter => _nextChapter;

        [SerializeField] 
        private bool _isLocked;
        
        [SerializeField] 
        private GameObject _lockedImage;
        
        private bool _isOpen;
        public EnemyQuestData[] EnemyQuestsData => _enemyQuestsData;
        public int Id;

        private void OnEnable()
        {
            if (_isLocked == false)
            {
                _lockedImage.SetActive(false);

                _chapterList.InitAllChapter();
                _chapterList.CloseAllChapters();
                Open();
            }
        }

        public void UnlockedChapter()
        {
            _isLocked = false;
            _lockedImage.SetActive(false);

           // Toggle();
        }

        private void Toggle()
        {
            if (_isLocked) return;

            _isOpen = !_isOpen;

            if (_isOpen)
            {
                _chapterList.CloseAllChapters();
                Open();
            }
            else
            {
                Close();
            }
        }

        private void Open()
        {
            _info.SetActive(true);
            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, OpenSize);
            _verticalLayoutGroup.spacing += 0.01f;
            _isOpen = true;
        }

        public void Close()
        {
            _info.SetActive(false);
            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, CloseSize);
            _verticalLayoutGroup.spacing -= 0.01f;
            _isOpen = false;
        }
    }
}