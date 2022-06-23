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

        [SerializeField] 
        private Image _frameImage;

        [SerializeField] 
        private Sprite[] _frames;

        [SerializeField] 
        private Animator _animator;
        
        private Sprite _sideSprite;
        private float startLocalScaleX;
        private Vector3 _startPosition;

        private void Start()
        {
            _shadow.gameObject.SetActive(false);
            _image.color = Color.clear;
            _magicCircleImage.color = Color.white;
            _frameImage.color = Color.clear;
            transform.localPosition = transform.localPosition.ToY(100);
            _startPosition = transform.localPosition;
        }

        public void Init(global::Card card)
        {
            _sideSprite = _image.sprite;
            startLocalScaleX = transform.localScale.x;
            _shadow.sprite = _image.sprite;
            
            if (_frameImage && _frames.Length != 0)
            {
                switch (card.Race)
                {
                    case RaceCard.Demons:
                        _frameImage.sprite = _frames[0];
                        break;
                        
                    case RaceCard.Gods:
                        _frameImage.sprite = _frames[1];
                        break;
                        
                    case RaceCard.Humans:
                        _frameImage.sprite = _frames[2];
                        break;
                }
            }
        }

        public void InitPosition(float y)
        {
            _startPosition = transform.localPosition;
            transform.localPosition = new Vector3(transform.localPosition.x, y, 0);
            _image.color = new Color(1, 1, 1, 1);
        }

        public void SetImage(Sprite uiIcon) => 
            _image.sprite = uiIcon;

        public IEnumerator StartingAnimation(Sequence sequence, float y)
        {
            var startScale = transform.localScale;
            var startShadowColor = _shadow.color;
            var scale = startScale / 1.3f;

            _shadow.sprite = _image.sprite;
            _image.color = Color.clear;
            _frameImage.color = Color.clear;
            //transform.localPosition = new Vector3(_startPosition.x, _startPosition.y + y, _startPosition.z);

            /*sequence
                //.Insert(0, transform.DOLocalMove(_startPosition, 1))
                .Insert(0, _magicCircleImage.transform.DOScale(_magicCircleImage.transform.localScale / 4, 1f))
                .Insert(2, _image.DOColor(Color.white, 1f))
                .Insert(2, _magicCircleImage.DOColor(Color.clear, 1f));
            */
            
            /* sequence
                //.Insert(0, _selectImage.DOColor(Color.white, 1f))
                //.Insert(0, _shadow.DOColor(Color.clear, 1f))
                //.Insert(1, _selectImage.DOColor(Color.clear, 1f))
                .Insert(0, transform.DOScale(startScale / 4, 1f))
                //.Insert(0, _image.DOColor(Color.clear, 1f))
                //.Insert(0, _magicCircleImage.DOColor(Color.white, 1f))
                //.Insert(3, _image.DOColor(Color.white, 1f))
                //.Insert(3, _shadow.DOColor(startShadowColor, 1f))
                .Insert(3, _magicCircleImage.DOColor(Color.clear, 1f))
                .Insert(3, transform.DOScale(scale, 0.5f))
                .Insert(3, _shadow.transform.DOLocalMove(Vector3.zero, 0.5f));
            */
            
            _animator.SetTrigger("Intro");
            yield return new WaitForSeconds(2f);

            print("Старт анимации");
            _smokeEffect.GetComponent<Image>().enabled = true;
            _smokeEffect.SetTrigger(_smoke);
            yield return new WaitForSeconds(1f);
            print("Конец анимации");
            _smokeEffect.GetComponent<Image>().enabled = false;
        }

        public void StartSmokeAnimation()
        {
            _smokeEffect.SetTrigger(_smoke);
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
            print(attackEffect);
            print(_effectContainer);
            var effect = Instantiate(attackEffect, _effectContainer);
            print(effect);
            effect.Play();
            yield return new WaitForSeconds(0.3f);
    
            var damageText = _damageTexts[0];
            damageText.text = '-' + attack.ToString();

            damageText.DOColor(new Color(1, 0, 0, 1), 0.3f);
            yield return new WaitForSeconds(1f);
            
            yield return Shake();

            yield return new WaitForSeconds(0.5f);
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