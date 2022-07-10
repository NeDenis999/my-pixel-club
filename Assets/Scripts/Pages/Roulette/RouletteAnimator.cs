using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Pages.Roulette
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

        [SerializeField] 
        private Image _prizeImage;
        
        [SerializeField] 
        private TextMeshProUGUI _prizeText;
        
        [SerializeField] 
        private UpPanel _upPanel;
        
        private Transform _currentParrent;
        private Vector3 _previousCurrentCellPosition;
        private Vector3 _previousCurrentCellScale;
        private RouletteCell _currentCell;

        public IEnumerator Spine(int prize, RouletteCell[] rouletteCells)
        {
            _upPanel.Block();
            
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

            _prizeImage.color = Color.white;
            _prizeImage.sprite = _currentCell.RouletteItem.UIIcon;
            _prizeText.text = _currentCell.RouletteItem.Description;
            var prizeView = _prizeImage.transform;
            _currentParrent = prizeView.transform.parent;
            _previousCurrentCellScale = prizeView.localScale;
            prizeView.localScale = Vector3.zero;
            prizeView.DOScale(_previousCurrentCellScale, 0.5f);
            //yield return new WaitForSeconds(0.5f);
            DOTween.To(() => _winningPanel.alpha, x => _winningPanel.alpha = x, 1, 1);
            yield return new WaitForSeconds(1);
            _winningPanel.interactable = true;
            _winningPanel.blocksRaycasts = true;
        }

        public IEnumerator CloseWinningPanel(Button startRoletteButton)
        {
            //yield return new WaitForSeconds(0.5f);
            //_currentCell.transform.parent = _currentParrent;
            DOTween.To(() => _winningPanel.alpha, x => _winningPanel.alpha = x, 0, 0.75f)
                .OnComplete(() =>
                {
                    _winningPanel.blocksRaycasts = false;
                    _winningPanel.interactable = false;
                });

            //_currentCell.transform.DOScale(_previousCurrentCellScale, 0.75f);
            _currentCell.Unselect();
            yield return new WaitForSeconds(0.75f);
            //_currentCell.transform.DOLocalMove(_previousCurrentCellPosition, 0.75f);
            yield return new WaitForSeconds(0.75f);
            startRoletteButton.interactable = true;
            _prizeImage.color = Color.clear;
            _upPanel.Unblock();
            _protectionFromExitingMenu.SetActive(false);
        }        
    }
}