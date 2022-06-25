using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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

        private void Start()
        {
            _shadow.gameObject.SetActive(false);
            _image.color = Color.clear;
            _magicCircleImage.color = Color.white;
            _frameImage.color = Color.clear;
            transform.localPosition = transform.localPosition.ToY(100);
        }

        public void Init(global::Card card)
        {
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

        public void InitPosition(float y) => 
            transform.localPosition = new Vector3(transform.localPosition.x, y, 0);

        public void SetImage(Sprite uiIcon) => 
            _image.sprite = uiIcon;

        public IEnumerator StartingAnimation(Sequence sequence, float y)
        {
            gameObject.SetActive(true);
            _shadow.sprite = _image.sprite;
            _image.color = Color.clear;
            _frameImage.color = Color.clear;

            _animator.SetTrigger("Intro");
            yield return new WaitForSeconds(2f);
            
            _smokeEffect.GetComponent<Image>().enabled = true;
            _smokeEffect.SetTrigger(_smoke);
            yield return new WaitForSeconds(1f);
            _smokeEffect.GetComponent<Image>().enabled = false;
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

        public void Selected()
        {
            var sequence = DOTween.Sequence();
            
            sequence
                .Insert(0, _selectImage.DOColor(new Color(1, 1, 1, 0.5f), 0.5f))
                .Insert(0.5f, _selectImage.DOColor(Color.clear, 0.5f));
        }

        public void Hide()
        {
            _image.color = Color.clear;
            _magicCircleImage.color = Color.white;
            _frameImage.color = Color.clear;
        }
    }
}