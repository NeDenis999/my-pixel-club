using System.Collections;
using System.Collections.Generic;
using Cards.Card;
using DG.Tweening;
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

        public IEnumerator AppearanceCards(CardAnimator[] enemyCardAnimators, CardAnimator[] playerCardAnimators, 
            List<int> playerCards, List<int> enemyCards)
        {
            StartCoroutine(ShowSideAllCards(enemyCardAnimators, 1000, _enemyHorizontalLayoutGroup, enemyCards));
            yield return ShowSideAllCards(playerCardAnimators, 1000, _playerHorizontalLayoutGroup, playerCards);
            yield return new WaitForSeconds(1f);
            yield return new WaitForSeconds(0.2f);
        }

        private IEnumerator ShowSideAllCards(CardAnimator[] cardAnimators, float y, HorizontalLayoutGroup horizontalLayoutGroup, List<int> cards)
        {
            var sequence = DOTween.Sequence();

            foreach (var cardAnimator in cardAnimators) 
                cardAnimator.gameObject.SetActive(false);

            foreach (var card in cards) 
                StartCoroutine(cardAnimators[card].StartingAnimation(sequence, y));

            yield return new WaitForSeconds(3f);
        }
    }
}