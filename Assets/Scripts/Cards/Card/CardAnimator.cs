using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Sequence = DG.Tweening.Sequence;

namespace Cards.Card
{
    public class CardAnimator : MonoBehaviour
    {
        private static readonly int _smoke = Animator.StringToHash("Smoke");

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
        private Image _shadow;

        [SerializeField] 
        private Image _selectImage;
        
        [SerializeField] 
        private Image _magicCircleImage;

        [SerializeField]
        private ParticleSystem _flyEffect;
        
        [SerializeField]
        private Animator _smokeEffect;

        [SerializeField]
        private Transform _effectContainer;

        [SerializeField] 
        private TextMeshProUGUI[] _damageTexts;

        private Sprite _sideSprite;
        private float startLocalScaleX;
        private Vector3 _startPosition;

        public void Init()
        {
            _sideSprite = _image.sprite;
            startLocalScaleX = transform.localScale.x;
            _shadow.sprite = _image.sprite;
        }

        public void InitPosition()
        {
            _startPosition = transform.localPosition;
            transform.localPosition = new Vector3(transform.localPosition.x, 1000, 0);
            _image.color = new Color(1, 1, 1, 1);
        }

        public void SetImage(Sprite uiIcon) => 
            _image.sprite = uiIcon;

        public IEnumerator StartingAnimation(Sequence sequence)
        {
            var startScale = transform.localScale;
            var startShadowColor = _shadow.color;
            var scale = startScale / 1.3f;
            
            sequence
                .Insert(0, _selectImage.DOColor(Color.white, 1f))
                .Insert(0, _shadow.DOColor(Color.clear, 1f))
                .Insert(1, _selectImage.DOColor(Color.clear, 1f))
                .Insert(1, transform.DOScale(startScale / 4, 1f))
                .Insert(1, _image.DOColor(Color.clear, 1f))
                .Insert(1, _magicCircleImage.DOColor(Color.white, 1f))
                .Insert(3, _image.DOColor(Color.white, 1f))
                .Insert(3, _shadow.DOColor(startShadowColor, 1f))
                .Insert(3, _magicCircleImage.DOColor(Color.clear, 1f))
                .Insert(4, transform.DOScale(scale, 0.5f))
                .Insert(4, _shadow.transform.DOLocalMove(Vector3.zero, 0.5f));
            
            yield return new WaitForSeconds(4f);

            print("Старт анимации");
            _smokeEffect.GetComponent<Image>().enabled = true;
            _smokeEffect.SetTrigger(_smoke);
            yield return new WaitForSeconds(1f);
            print("Конец анимации");
            _smokeEffect.GetComponent<Image>().enabled = false;
            
        }
        
        public IEnumerator ShowSide()
        {
            print("ShowSide");
            transform.DOLocalMove(_startPosition, 2f);
            yield return new WaitForSeconds(2f);
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

        public IEnumerator Hit(ParticleSystem attackEffect, int attack)
        {
            var effect = Instantiate(attackEffect, _effectContainer);
            effect.Play();
            //_effects[Random.Range(0, _effects.Length)].Play();
            yield return new WaitForSeconds(0.3f);
    
            var damageText = _damageTexts[0];
            damageText.text = attack.ToString();
            
            yield return Shake();

            damageText.DOColor(new Color(1, 0, 0, 1), 0.3f);
            yield return new WaitForSeconds(1f);

            damageText.DOColor(new Color(1, 0, 0, 0), 0.3f);
            yield return new WaitForSeconds(0.5f);
            
            Destroy(effect);
        }

        private IEnumerator Shake()
        {
            var startLocalPosition = transform.localPosition;
            
            for (int i = 0; i < 10; i++)
            {
                var multiplier = 1 - (i / 9);

                transform.DOLocalMove(transform.localPosition.RandomVector2(4 * multiplier), 0.005f);
                yield return new WaitForSeconds(0.005f);
                transform.DOLocalMove(startLocalPosition, 0.005f);
                yield return new WaitForSeconds(0.005f);
            }
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

        public void Selected()
        {
            var sequence = DOTween.Sequence();
            
            sequence
                .Insert(0, _selectImage.DOColor(new Color(1, 1, 1, 0.5f), 0.5f))
                .Insert(0.5f, _selectImage.DOColor(Color.clear, 0.5f));
        }
    }
}