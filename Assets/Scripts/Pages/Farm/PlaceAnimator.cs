using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Pages.Farm
{
    public class PlaceAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private const float ScalingFactor = 1.2f;
        private const float DurationScaling = 0.2f;
        private const float WaitBeforeScale = 0.1f;

        [SerializeField] 
        private Transform _transform;
        
        private Sequence _sequence;
        private Vector3 _selectSize;
        private Vector3 _normalSize;

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

        private void UnSelected()
        {
            UpdateSequence();
            _sequence.Insert(0, _transform.DOScale(_normalSize, DurationScaling));
        }

        private void UpdateSequence()
        {
            _sequence.Kill();
            _sequence = DOTween.Sequence();
        }

        private void Pressed()
        {
            
        }
    }
}