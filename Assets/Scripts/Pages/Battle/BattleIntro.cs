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

        private Vector3 _startObstacleScale;
        private Vector3 _startTurnTextScale;

        public void Initialization()
        {
            _startObstacleScale = _obstacle.transform.localScale;
            _startTurnTextScale = _turnText.transform.localScale;
        }

        public IEnumerator Intro(string text)
        {
            gameObject.SetActive(true);
            _turnText.text = text;

            var sequence = DOTween.Sequence();

            _obstacle.transform.localScale *= 3;
            _turnText.transform.localScale /= 3;
            _obstacle.color = Color.clear;
            _turnText.color = Color.clear;


            sequence
                .Insert(0, _obstacle.DOColor(Color.white, 0.3f))
                .Insert(0, _turnText.DOColor(Color.red, 0.3f))
                .Insert(0, _obstacle.transform.DOScale(_startObstacleScale, 0.5f))
                .Insert(0, _turnText.transform.DOScale(_startTurnTextScale, 0.5f))
                .Insert(0, _obstacle.transform.DORotate(new Vector3(0, 0, 45), 3f))

                .Insert(1f, _obstacle.transform.DOScale(_startTurnTextScale * 0.9f, 2))
                .Insert(1f, _turnText.transform.DOScale(_startTurnTextScale * 1.1f, 2))
                //.Insert(2.5f, _obstacle.transform.DORotate(new Vector3(0, 0, 40), 0.5f))

                .Insert(3f, _obstacle.transform.DORotate(new Vector3(0, 0, 0), 0.5f))
                .Insert(3f, _obstacle.transform.DOScale(_startTurnTextScale * 2f, 0.5f))
                .Insert(3f, _turnText.transform.DOScale(_startTurnTextScale / 2f, 0.5f))
                .Insert(3f, _obstacle.DOColor(Color.clear, 0.5f))
                .Insert(3f, _turnText.DOColor(Color.clear, 0.5f));
                //.Insert(4f, _obstacle.transform.DORotate(new Vector3(0, 0, -30), 0.5f));
            yield return new WaitForSeconds(3.5f);
        }

        public IEnumerator EndIntro() => 
            SwitchTurnIntro("You Win");

        public IEnumerator SwitchTurnIntro(string text)
        {
            yield return Intro(text);
            /*var sequence = DOTween.Sequence();
            _finishText.text = text;
            var imageStartPosition = _background.transform.localPosition;
            var textStartPosition = _finishText.transform.localPosition;

            _background.transform.localPosition = imageStartPosition.ToX(100);
            _finishText.transform.localPosition = textStartPosition.ToX(-100);

            sequence
                .Insert(0, _background.DOColor(new Color(0, 0, 0, 0.5f), 0.5f))
                .Insert(0, _finishText.DOColor(new Color(1, 0, 0, 1), 0.5f))
                .Insert(0, _background.transform.DOLocalMove(imageStartPosition, 0.5f))
                .Insert(0, _finishText.transform.DOLocalMove(textStartPosition, 0.5f))
                .Insert(1, _background.DOColor(Color.clear, 0.5f))
                .Insert(1, _finishText.DOColor(Color.clear, 0.5f));
            
            yield return new WaitForSeconds(1.5f);*/
        }
    }
}