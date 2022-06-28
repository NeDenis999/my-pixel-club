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

        [SerializeField] 
        private Sprite _playerObstacle;
        
        [SerializeField] 
        private Sprite _enemyObstacle;
        
        private Vector3 _startObstacleScale;
        private Vector3 _startTurnTextScale;

        public void Initialization()
        {
            _startObstacleScale = _obstacle.transform.localScale;
            _startTurnTextScale = _turnText.transform.localScale;
        }

        private IEnumerator Intro(string text, Sprite obstacle)
        {
            gameObject.SetActive(true);
            _obstacle.sprite = obstacle;
            _turnText.text = text;

            var sequence = DOTween.Sequence();

            _obstacle.transform.localScale *= 3;
            _turnText.transform.localScale /= 3;
            _obstacle.color = Color.clear;
            _turnText.color = Color.clear;

            float m = 0.5f;
            
            sequence
                .Insert(0, _obstacle.DOColor(Color.white, 0.3f * m))
                .Insert(0, _turnText.DOColor(Color.red, 0.3f))
                .Insert(0, _obstacle.transform.DOScale(_startObstacleScale, 0.5f * m))
                .Insert(0, _turnText.transform.DOScale(_startTurnTextScale, 0.5f * m))
                .Insert(0, _obstacle.transform.DORotate(new Vector3(0, 0, 45), 3f * m))

                .Insert(1f * m, _obstacle.transform.DOScale(_startTurnTextScale * 0.9f, 2 * m))
                .Insert(1f * m, _turnText.transform.DOScale(_startTurnTextScale * 1.1f, 2 * m))

                .Insert(3f * m, _obstacle.transform.DORotate(new Vector3(0, 0, 0), 0.5f * m))
                .Insert(3f * m, _obstacle.transform.DOScale(_startTurnTextScale * 2f, 0.5f * m))
                .Insert(3f * m, _turnText.transform.DOScale(_startTurnTextScale / 2f, 0.5f * m))
                .Insert(3f * m, _obstacle.DOColor(Color.clear, 0.5f * m))
                .Insert(3f * m, _turnText.DOColor(Color.clear, 0.5f * m));
            
            yield return new WaitForSeconds(3.5f * m);
            
            _obstacle.transform.localScale = _startObstacleScale;
            _turnText.transform.localScale = _startTurnTextScale;
        }

        public IEnumerator PlayerTurn() => 
            Intro("Player Turn", _playerObstacle);
        
        public IEnumerator OpponentTurn() => 
            Intro("Opponent Turn", _enemyObstacle);
        
        public IEnumerator EndIntro() => 
            Intro("You Win", _playerObstacle);
    }
}