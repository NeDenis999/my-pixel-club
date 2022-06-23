using System.Collections;
using Cards.Card;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class BattleAnimator : MonoBehaviour
    {
        [SerializeField] 
        private HorizontalLayoutGroup _enemyHorizontalLayoutGroup;
        
        [SerializeField] 
        private HorizontalLayoutGroup _playerHorizontalLayoutGroup;

        public IEnumerator AppearanceCards(CardAnimator[] enemyCardAnimators, CardAnimator[] playerCardAnimators)
        {
            StartCoroutine(ShowSideAllCards(enemyCardAnimators, 1000, _enemyHorizontalLayoutGroup));
            yield return ShowSideAllCards(playerCardAnimators, 1000, _playerHorizontalLayoutGroup);
            yield return new WaitForSeconds(1f);
            yield return new WaitForSeconds(0.2f);
            print("Анимация закончилась");
        }

        public IEnumerator HitAllCards(CardAnimator[] cardAnimators)
        {
            _enemyHorizontalLayoutGroup.enabled = false;
            
            //foreach (var cardAnimator in cardAnimators) 
                //StartCoroutine(cardAnimator.Hit());

            yield return new WaitForSeconds(4);
            
            _enemyHorizontalLayoutGroup.enabled = true;
        }

        private void SpreadCards(HorizontalLayoutGroup layoutGroup) => 
            DOTween.To(() => layoutGroup.spacing, x => layoutGroup.spacing = x, 5, 0.5f);

        private IEnumerator ShowSideAllCards(CardAnimator[] cardAnimators, float y, HorizontalLayoutGroup horizontalLayoutGroup)
        {
            var sequence = DOTween.Sequence();
            
            //yield return new WaitForSeconds(0.1f);
            //horizontalLayoutGroup.enabled = false;

            //var startPosition = horizontalLayoutGroup.transform.localPosition;
            //horizontalLayoutGroup.transform.localPosition = horizontalLayoutGroup.transform.localPosition.ToY(y);
            //horizontalLayoutGroup.transform.DOLocalMove(startPosition, 1);
            //yield return new WaitForSeconds(0.1f);
            
            foreach (var cardAnimator in cardAnimators)
            {
                StartCoroutine(cardAnimator.StartingAnimation(sequence, y));
                //yield return new WaitForSeconds(0.2f);
            }
                
            
            yield return new WaitForSeconds(2f);
            
            /*
            sequence
                .Insert(0, DOTween.To(() =>
                        horizontalLayoutGroup.spacing,
                    x => horizontalLayoutGroup.spacing = x,
                    -526.5f, 0.5f))
                .Insert(0, horizontalLayoutGroup.transform.DOLocalMoveY(
                    horizontalLayoutGroup.transform.localPosition.y + y, 0.5f));
*/
            
            yield return new WaitForSeconds(1f);
            print("Колода раскрылась");
        }

        public void InitPositionAllCards(CardAnimator[] cardAnimators)
        {
            foreach (var cardAnimator in cardAnimators) 
                cardAnimator.InitPosition(1000);
        }

        private IEnumerator ShowStateAllCards(CardAnimator[] cardAnimators)
        {
            foreach (var cardAnimator in cardAnimators)
            {
                StartCoroutine(cardAnimator.ShowState());
            }
        
            yield return new WaitForSeconds(1);
        }
    }
}