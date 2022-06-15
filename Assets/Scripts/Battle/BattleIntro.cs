using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class BattleIntro : MonoBehaviour
    {
        [SerializeField] 
        private Image _obstacle;

        [SerializeField] 
        private TextMeshProUGUI _turnText;
        
        [SerializeField] 
        private Image _background;

        [SerializeField] 
        private TextMeshProUGUI _finishText;
        
        public IEnumerator Intro(string text)
        {
            gameObject.SetActive(true);
            _turnText.text = text;
            
            var sequence = DOTween.Sequence();
            var startObstacleScale = _obstacle.transform.localScale;
            var startTurnTextScale = _turnText.transform.localScale;
            _obstacle.transform.localScale *= 3;
            _turnText.transform.localScale /= 3;
            _obstacle.color = Color.clear;
            _turnText.color = Color.clear;
            
            sequence
                .Insert(0, _obstacle.DOColor(Color.white, 0.3f))
                .Insert(0, _turnText.DOColor(Color.red, 0.3f))
                .Insert(0, _obstacle.transform.DOScale(startObstacleScale, 1))
                .Insert(0, _turnText.transform.DOScale(startTurnTextScale, 1))
                .Insert(0.9f, _obstacle.transform.DORotate(new Vector3(0, 0, 45), 2.3f))
                .Insert(1, _obstacle.transform.DOScale(startTurnTextScale * 0.9f, 2))
                .Insert(1, _turnText.transform.DOScale(startTurnTextScale * 1.1f, 2))
                .Insert(2.9f, _obstacle.transform.DOScale(startTurnTextScale * 3f, 1))
                .Insert(3, _turnText.transform.DOScale(startTurnTextScale / 3f, 1))
                .Insert(3.5f, _obstacle.DOColor(Color.clear, 0.7f))
                .Insert(3.7f, _turnText.DOColor(Color.clear, 0.5f));
            
            yield return new WaitForSeconds(5);
        }

        public IEnumerator EndIntro()
        {
            var sequence = DOTween.Sequence();

            sequence
                .Insert(0, _background.DOColor(new Color(0, 0, 0, 0.5f), 0.5f))
                .Insert(0, _finishText.DOColor(new Color(1, 0, 0, 1), 0.5f))
                .Insert(1, _background.DOColor(Color.clear, 0.5f))
                .Insert(1, _finishText.DOColor(Color.clear, 0.5f));
            
            yield return new WaitForSeconds(1.5f);
        }
    }
}