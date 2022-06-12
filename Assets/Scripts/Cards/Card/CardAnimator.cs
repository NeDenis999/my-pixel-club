using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Cards.Card
{
    public class CardAnimator : MonoBehaviour
    {
        [SerializeField] 
        private Image _image;

        [SerializeField] 
        private Sprite _sideBackSprite;

        [SerializeField] 
        private Image _lightImage;
        
        [SerializeField] 
        private Image _hitImage;

        [SerializeField] 
        private TextMeshProUGUI[] _damageTexts;
        
        private Sprite _sideSprite;
        private float startLocalScaleX;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _sideSprite = _image.sprite;
            _image.sprite = _sideBackSprite;
            startLocalScaleX = transform.localScale.x;
        }

        public IEnumerator ShowSide()
        {
            transform.DOScaleX(0, 0.4f);
            yield return new WaitForSeconds(0.4f);
            _image.sprite = _sideSprite;
            transform.DOScaleX(startLocalScaleX, 0.4f);
            yield return new WaitForSeconds(0.4f);
        }

        public IEnumerator Hit()
        {
            _hitImage.DOColor(new Color(1, 1, 1, 1), 0.05f);
            yield return new WaitForSeconds(0.2f);

            var startLocalPosition = transform.localPosition;
            
            for (int i = 0; i < 10; i++)
            {
                var multiplier = 1 - (i / 9);
                
                transform.DOLocalMove(transform.localPosition.RandomVector2(4 * multiplier), 0.005f);
                yield return new WaitForSeconds(0.005f);
                transform.DOLocalMove(startLocalPosition, 0.005f);
                yield return new WaitForSeconds(0.005f);
            }

            yield return new WaitForSeconds(0.5f);
            _hitImage.DOColor(new Color(1, 1, 1, 0), 0.05f);
            yield return new WaitForSeconds(0.1f);
            
            foreach (var damageText in _damageTexts)
            {
                StartCoroutine(TextEffect(damageText));
                yield return new WaitForSeconds(0.4f);
            }
            
            yield return new WaitForSeconds(10);
        }

        private static IEnumerator TextEffect(TextMeshProUGUI _damageText)
        {
            var startPosition = _damageText.transform.localPosition;
            
            _damageText.DOColor(new Color(1, 0, 0, 1), 0.5f);
            yield return new WaitForSeconds(0.3f);
            _damageText.transform.DOMoveY(_damageText.transform.position.y - 80, 1);
            yield return new WaitForSeconds(0.5f);
            _damageText.DOColor(new Color(1, 0, 0, 0), 0.5f);
            yield return new WaitForSeconds(0.5f);

            _damageText.transform.localPosition = startPosition;
        }
    }
}