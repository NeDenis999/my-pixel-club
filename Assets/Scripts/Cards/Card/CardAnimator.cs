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
        private Image _stateImage;
        
        [SerializeField] 
        private TextMeshProUGUI[] _damageTexts;
        
        private Sprite _sideSprite;
        private float startLocalScaleX;
        private Vector3 _startPosition;

        private void Start()
        {
            //Init();
        }

        public void Init()
        {
            _sideSprite = _image.sprite;
            //_image.sprite = _sideBackSprite;
            _image.color = new Color(1, 1, 1, 0);
            startLocalScaleX = transform.localScale.x;
            
        }

        public void InitPosition()
        {
            _startPosition = transform.localPosition;
            transform.localPosition = new Vector3(transform.localPosition.x, 1000, 0);
            _image.color = new Color(1, 1, 1, 1);
        }

        public void SetImage(Sprite uiIcon) => 
            _image.sprite = uiIcon;

        public IEnumerator ShowSide()
        {
            print("ShowSide");
            transform.DOLocalMove(_startPosition, 2f);
            yield return new WaitForSeconds(2f);

            /*transform.DOScaleX(0, 0.4f);
            yield return new WaitForSeconds(0.4f);
            _image.sprite = _sideSprite;
            transform.DOScaleX(startLocalScaleX, 0.4f);
            yield return new WaitForSeconds(0.4f);*/
        }

        public IEnumerator ShowState()
        {
            _lightImage.DOColor(new Color(0, 1, 0, 0.60f), 1);
            _stateImage.DOColor(new Color(1, 1, 1, 1f), 1);
            yield return new WaitForSeconds(2f);
            _lightImage.DOColor(new Color(0, 1, 0, 0), 1);
            _stateImage.DOColor(new Color(1, 1, 1, 0), 1);
            yield return new WaitForSeconds(1f);
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