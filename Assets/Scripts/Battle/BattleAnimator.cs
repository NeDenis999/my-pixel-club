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

        [SerializeField]
        private CardAnimator[] _playerCardAnimators;

        public IEnumerator AppearanceCards(CardAnimator[] enemyCardAnimators)
        {
            _enemyHorizontalLayoutGroup.spacing = -850;
            _playerHorizontalLayoutGroup.spacing = -520;
            yield return new WaitForSeconds(1);
            SpreadCards(_enemyHorizontalLayoutGroup);
            yield return new WaitForSeconds(1);
            yield return StartCoroutine(ShowSideAllCards(enemyCardAnimators));
        }

        private void SpreadCards(HorizontalLayoutGroup layoutGroup) => 
            DOTween.To(() => layoutGroup.spacing, x => layoutGroup.spacing = x, 5, 1);

        private IEnumerator ShowSideAllCards(CardAnimator[] cardAnimators)
        {
            for (int i = 0; i < (cardAnimators.Length + 1) / 2; i++)
            {
                if (cardAnimators[i] != cardAnimators[cardAnimators.Length - 1 - i])
                {
                    print(i + "/" + (cardAnimators.Length - 1));
                    StartCoroutine(cardAnimators[i].ShowSide());
                    yield return StartCoroutine(cardAnimators[cardAnimators.Length - 1 - i].ShowSide());    
                }
                else
                {
                    print(i + "/" + (cardAnimators.Length - 1));
                    yield return StartCoroutine(cardAnimators[i].ShowSide());    
                }
            }
        }
    }
}