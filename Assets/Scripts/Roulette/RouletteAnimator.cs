using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Roulette
{
    public class RouletteAnimator : MonoBehaviour
    {
        [SerializeField]
        private float _braking = 1.5f;

        [SerializeField]
        private CanvasGroup _winningPanel;
        
        [SerializeField]
        private Transform _target;

        [SerializeField] 
        private float _startSpeedRotation;

        private float _currentSpeedRotation;
        private Transform _currentParrent;
        private Vector3 _previousCurrentCellPosition;
        private Vector3 _previousCurrentCellScale;

        private RouletteCell _currentCell;

        public IEnumerator Spine(int price, RouletteCell[] rouletteCells)
        {
            _currentCell = rouletteCells[price];
            _currentSpeedRotation = _startSpeedRotation;
            
            var currentCellNumber = 0;
            var isCirclePassed = false;

            while (price != currentCellNumber || !isCirclePassed)
            {
                if (currentCellNumber < rouletteCells.Length - 1)
                    currentCellNumber++;
                else
                {
                    isCirclePassed = true;
                    currentCellNumber = 0;
                }

                UnselectedAllCells(rouletteCells);
                rouletteCells[currentCellNumber].Select();

                if (_currentSpeedRotation < _braking && isCirclePassed)
                    _currentSpeedRotation *= 1.2f;

                yield return new WaitForSeconds(_currentSpeedRotation);
            }
            
            _currentParrent = _currentCell.transform.parent;

            _previousCurrentCellPosition = _currentCell.transform.localPosition;
            _previousCurrentCellScale = _currentCell.transform.localScale;
            _currentCell.transform.parent = _target;
            _currentCell.transform.DOLocalMove(Vector3.zero, 1);
            yield return new WaitForSeconds(1);
            _currentCell.transform.DOScale(new Vector3(30, 30, 1), 1);
            yield return new WaitForSeconds(1);
            DOTween.To(() => _winningPanel.alpha, x => _winningPanel.alpha = x, 1, 1);
            yield return new WaitForSeconds(1);
            _winningPanel.interactable = true;
            _winningPanel.blocksRaycasts = true;
        }

        public IEnumerator CloseWinningPanel(Button startRoletteButton)
        {
            _currentCell.transform.parent = _currentParrent;
            DOTween.To(() => _winningPanel.alpha, x => _winningPanel.alpha = x, 0, 0.75f)
                .OnComplete(() =>
                {
                    _winningPanel.blocksRaycasts = false;
                    _winningPanel.interactable = false;
                });

            _currentCell.transform.DOScale(_previousCurrentCellScale, 0.75f);
            _currentCell.Unselect();
            yield return new WaitForSeconds(0.75f);
            _currentCell.transform.DOLocalMove(_previousCurrentCellPosition, 0.75f);
            yield return new WaitForSeconds(0.75f);
            startRoletteButton.interactable = true;
        }

        private void UnselectedAllCells(RouletteCell[] rouletteCells)
        {
            foreach (var rouletteCell in rouletteCells)
                rouletteCell.Unselect();
        }
    }
}