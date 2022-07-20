using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace FarmPage.Shop
{
    public class ConfirmWindowAnimator : MonoBehaviour 
    {
        private const float ScaleMultiplier = 1.2f;
        
        [SerializeField] 
        private Image _itemViewImage;
        
        [SerializeField] 
        private Image _lightImage;
        
        private Sequence _sequence;
        private Vector2 _startScale;
        
        private void Start()
        {
            _startScale = transform.localScale;
        }
        
        public void PlayBuyAnimation()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            _sequence.Insert(0, _itemViewImage.transform.DOScale(_startScale * ScaleMultiplier, 0.2f));
            _sequence.Insert(0, _lightImage.DOColor(new Color(1, 1, 0, 0.3f), 0.2f));
            _sequence.Insert(0.2f, _itemViewImage.transform.DOScale(_startScale, 0.3f));
            _sequence.Insert(0.2f, _lightImage.DOColor(new Color(1, 1, 0, 0), 0.3f));
        }
    }
}