using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Cards.Card
{
    public class CardAnimator : MonoBehaviour
    {
        [SerializeField] 
        private Image _image;

        [SerializeField] 
        private Sprite _sideBackSprite;

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
            transform.DOScaleX(0, 1);
            yield return new WaitForSeconds(1);
            _image.sprite = _sideSprite;
            transform.DOScaleX(startLocalScaleX, 1);
            yield return new WaitForSeconds(1);
        }
    }
}