using UnityEngine;
using UnityEngine.UI;

namespace Pages.Quest
{
    public class Chapter : MonoBehaviour
    {
        private const float _openSize = 471.36f;
        private const float _closeSize = 100f;

        [SerializeField]
        private RectTransform _rectTransform;

        [SerializeField]
        private GameObject _info;

        [SerializeField]
        private VerticalLayoutGroup _verticalLayoutGroup;

        [SerializeField]
        private ChapterList _chapterList;

        [SerializeField] private Enemy _enemy;

        [SerializeField] private bool _isLocked;
        private bool _isOpen;

        public Enemy Enemy => _enemy;

        public void UnlockedChapter()
        {
            _isLocked = false;
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
            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, _openSize);
            _verticalLayoutGroup.spacing += 0.01f;
            _isOpen = true;
        }

        public void Close()
        {
            _info.SetActive(false);
            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, _closeSize);
            _verticalLayoutGroup.spacing -= 0.01f;
            _isOpen = false;
        }
    }
}