using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace FarmPage.StartScreen
{
    public class StartScreenTextAnimator : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _text;

        private Sequence _sequence;
        
        private void Start()
        {
            StartAnimation();
        }

        private void StartAnimation()
        {
            _sequence = DOTween.Sequence();
            
            _sequence
                .Insert(0f, _text.DOColor(Color.white, 0.5f))
                .Insert(0.5f, _text.DOColor(new Color(0.5f, 0.5f, 0.5f, 1), 0.5f))
                .Insert(1f, _text.DOColor(Color.white, 0.5f))
                .SetLoops(-1);
        }

        private void OnApplicationQuit() => 
            _sequence.Kill();
    }
}