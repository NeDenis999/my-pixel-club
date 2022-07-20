using System.Collections;
using Cards.CardsCell;
using DG.Tweening;
using UnityEngine;

namespace FarmPage.Shop
{
    public class PurchaseWindow : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private Transform _container;

        [SerializeField]
        private CardDisplay _cardDisplay;

        private Sequence _sequence;

        public void StartOpen(ShopItem amountItems, Card[] cards)
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 0;
            StartCoroutine(Open(amountItems, cards));
        }

        public void StartClose()
        {
            StartCoroutine(Close());
        }
        private IEnumerator Open(ShopItem amountItems, Card[] cards)
        {
            foreach (Transform item in _container)
                Destroy(item.gameObject);

            yield return new WaitForSeconds(0.2f);
            _sequence.Kill();
            _sequence = DOTween.Sequence();
            
            _sequence.Insert(0, DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1, 0.75f));

            for (int i = 0; i < cards.Length; i++)
            {
                var cardCell = Instantiate(_cardDisplay, _container);
                cardCell.UpdateDisplay(cards[i]);
            }
        }
        
        private IEnumerator Close()
        {
            _sequence.Kill();
            _sequence = DOTween.Sequence();
            
            _sequence.Insert(0, DOTween
                .To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0, 0.75f)
                .OnComplete(() => gameObject.SetActive(false)));
            
            yield return new WaitForSeconds(0.2f);
        }
    }
}