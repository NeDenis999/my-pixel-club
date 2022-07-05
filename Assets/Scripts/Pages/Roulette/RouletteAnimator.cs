using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Roulette
{
    public class RouletteAnimator : MonoBehaviour
    {
        [SerializeField]
        private float _braking;

        [SerializeField]
        private CanvasGroup _winningPanel;
        
        [SerializeField]
        private Transform _target;

        [SerializeField] 
        private GameObject _protectionFromExitingMenu;
        
        private Transform _currentParrent;
        private Vector3 _previousCurrentCellPosition;
        private Vector3 _previousCurrentCellScale;

        private RouletteCell _currentCell;

        public IEnumerator Spine(int prize, RouletteCell[] rouletteCells)
        {
            _protectionFromExitingMenu.SetActive(true);
            float rotationSpeed = 0.1f;

            _currentCell = rouletteCells[prize];
            
            int currentCellNumber = 0;
            bool isCirclePassed = false;

            while (prize != currentCellNumber || isCirclePassed == false)
            {
                rouletteCells[currentCellNumber].Unselect();

                if (currentCellNumber < rouletteCells.Length - 1)
                {
                    currentCellNumber++;
                }
                else
                {
                    isCirclePassed = true;
                    currentCellNumber = 0;
                }

                rouletteCells[currentCellNumber].Select();

                if (rotationSpeed < _braking && isCirclePassed)
                    rotationSpeed *= 1.2f;

                yield return new WaitForSeconds(rotationSpeed);
            }


            
            _currentParrent = _currentCell.transform.parent;

            _previousCurrentCellPosition = _currentCell.transform.localPosition;
            _previousCurrentCellScale = _currentCell.transform.localScale;
            _currentCell.transform.parent = _target;
            _currentCell.transform.DOMove(_target.position, 1);
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
            _protectionFromExitingMenu.SetActive(false);
        }        
    }
}