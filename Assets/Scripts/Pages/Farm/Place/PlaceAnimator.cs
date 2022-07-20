using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FarmPage.Farm
{
    public class PlaceAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private const float ScalingFactor = 1.2f;
        private const float DurationScaling = 0.2f;
        private const float WaitBeforeScale = 0.1f;

        [SerializeField] 
        private Transform _transform;

        [SerializeField]
        private RectTransform _selectRectTransform;

        private Sequence _sequence;
        private Vector3 _selectSize;
        private Vector3 _normalSize;
        private bool _pressed;

        private void Awake()
        {
            var localScale = _transform.localScale;

            _normalSize = localScale;
            _selectSize = localScale * ScalingFactor;
        }

        public void OnPointerEnter(PointerEventData eventData) => 
            Selected();

        public void OnPointerExit(PointerEventData eventData) => 
            UnSelected();

        private void Selected()
        {
            UpdateSequence();
            _sequence.Insert(WaitBeforeScale, _transform.DOScale(_selectSize, DurationScaling));
        }

        public void UnSelected()
        {
            if (_pressed)
                return;
            
            UpdateSequence();
            _sequence.Insert(0, _transform.DOScale(_normalSize, DurationScaling));
        }

        private void UpdateSequence()
        {
            _sequence.Kill();
            _sequence = DOTween.Sequence();
        }

        public void Pressed()
        {
            _selectRectTransform.gameObject.SetActive(true);
            UpdateSequence();
            _pressed = true;
        }
        
        public void Unpressed()
        {
            _selectRectTransform.gameObject.SetActive(false);
            UpdateSequence();
            _pressed = false;
        }
    }
}