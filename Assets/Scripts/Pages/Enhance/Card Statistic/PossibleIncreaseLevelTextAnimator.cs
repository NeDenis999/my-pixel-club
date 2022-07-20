using DG.Tweening;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using Sequence = DG.Tweening.Sequence;

namespace FarmPage.Enhance.Card_Statistic
{
    public class PossibleIncreaseLevelTextAnimator : MonoBehaviour
    {
        private const float ScaleMultiplier = 1.2f;

        [SerializeField]
        private TMP_Text _text;

        [SerializeField] 
        private Image _lightImage;
        
        private Sequence _sequence;
        private Vector2 _startScale;

        private void Start()
        {
            _startScale = transform.localScale;
        }
        
        public void LevelUp(string text)
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            _text.text = text;
            _sequence.Insert(0, _text.transform.DOScale(_startScale * ScaleMultiplier, 0.2f));
            _sequence.Insert(0, _lightImage.DOColor(new Color(1, 1, 0, 0.3f), 0.2f));
            _sequence.Insert(0.2f, _text.transform.DOScale(_startScale, 0.3f));
            _sequence.Insert(0.2f, _lightImage.DOColor(new Color(1, 1, 0, 0), 0.3f));
        }

        public void Reset()
        {
            _sequence?.Kill();
            _text.text = "";
            _text.transform.localScale = _startScale;
            _lightImage.color = Color.clear;
        }

        private void OnApplicationQuit() => 
            _sequence?.Kill();
    }
}