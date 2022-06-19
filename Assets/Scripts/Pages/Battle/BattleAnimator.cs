﻿using System.Collections;
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
            //_enemyHorizontalLayoutGroup.spacing = -850;
            //_playerHorizontalLayoutGroup.spacing = -520;
            
            //yield return new WaitForSeconds(0.1f);
            //_enemyHorizontalLayoutGroup.enabled = false;
            //InitPositionAllCards(enemyCardAnimators);
            StartCoroutine(ShowSideAllCards(enemyCardAnimators, 50, _enemyHorizontalLayoutGroup));
            yield return ShowSideAllCards(playerCardAnimators, -50, _playerHorizontalLayoutGroup);
            yield return new WaitForSeconds(1f);
            //yield return ShowStateAllCards(enemyCardAnimators);
            //SpreadCards(_enemyHorizontalLayoutGroup);
            yield return new WaitForSeconds(0.2f);
            print("Анимация закончилась");
            
            //yield return new WaitForSeconds(0.1f);
            /*SpreadCards(_playerHorizontalLayoutGroup);
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(ShowSideAllCards(_playerCardAnimators));*/
        }

        public IEnumerator HitAllCards(CardAnimator[] cardAnimators)
        {
            _enemyHorizontalLayoutGroup.enabled = false;
            
            foreach (var cardAnimator in cardAnimators) 
                StartCoroutine(cardAnimator.Hit());

            yield return new WaitForSeconds(4);
            
            _enemyHorizontalLayoutGroup.enabled = true;
        }

        private void SpreadCards(HorizontalLayoutGroup layoutGroup) => 
            DOTween.To(() => layoutGroup.spacing, x => layoutGroup.spacing = x, 5, 0.5f);

        private IEnumerator ShowSideAllCards(CardAnimator[] cardAnimators, float y, HorizontalLayoutGroup horizontalLayoutGroup)
        {
            var sequence = DOTween.Sequence();

            sequence
                .Insert(0, DOTween.To(() =>
                        horizontalLayoutGroup.spacing,
                    x => horizontalLayoutGroup.spacing = x,
                    -526.5f, 0.5f))
                .Insert(0, horizontalLayoutGroup.transform.DOLocalMoveY(
                    horizontalLayoutGroup.transform.localPosition.y + y, 0.5f));
            
            foreach (var cardAnimator in cardAnimators) 
                StartCoroutine(cardAnimator.StartingAnimation(sequence));

            /*for (int i = 0; i < (cardAnimators.Length + 1) / 2; i++)
            {
                if (cardAnimators[i] != cardAnimators[cardAnimators.Length - 1 - i])
                {
                    print(i + "/" + (cardAnimators.Length - 1));
                    StartCoroutine(cardAnimators[i].ShowSide());
                    StartCoroutine(cardAnimators[cardAnimators.Length - 1 - i].ShowSide());
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    print(i + "/" + (cardAnimators.Length - 1));
                    yield return StartCoroutine(cardAnimators[i].ShowSide());
                    yield return new WaitForSeconds(1f);
                }
            }*/
            
            yield return new WaitForSeconds(1f);
            print("Колода раскрылась");
        }

        public void InitPositionAllCards(CardAnimator[] cardAnimators)
        {
            foreach (var cardAnimator in cardAnimators) 
                cardAnimator.InitPosition();
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