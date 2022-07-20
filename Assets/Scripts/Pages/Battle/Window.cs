using DG.Tweening;
using UnityEngine;

namespace FarmPage.Battle
{
    public class Window : MonoBehaviour
    {
        [SerializeField] 
        private CanvasGroup _canvasGroup;
        
        private Vector3 _startPosition;
        private Sequence _sequence;
        
        public void HideSmooth()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
        
            _sequence
                .Insert(0, DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0, 0.3f))
                .Insert(0, transform.DOLocalMove(_startPosition + new Vector3(0, -120, 0), 0.3f))
                .OnComplete(() => gameObject.SetActive(false));
        }
    
        public void ShowSmooth()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 0;
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
        
            _canvasGroup.alpha = 0;
            transform.localPosition = _startPosition + new Vector3(0, 120, 0);
            _sequence
                .Insert(0, DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1, 0.6f))
                .Insert(0, transform.DOLocalMove(_startPosition, 0.5f));
        }
    }
}