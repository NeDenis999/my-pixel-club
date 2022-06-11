using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Roulette
{
    public class RouletteScreen : MonoBehaviour
    {
        public event UnityAction<Card[]> OnReceivedCard;
        public event UnityAction<int> OnReceivedCristal;
        public event UnityAction<int> OnReceivedGold;
        public event UnityAction<int> OnBuyRouletteSpin;

        [SerializeField] 
        private RouletteCell[] _rouletteCells;
        
        [SerializeField] 
        private int _spinePrise;

        [SerializeField] 
        private Button _startRoletteButton;
        
        [SerializeField] 
        private Button _collectButton;

        [SerializeField] 
        private RouletteAnimator _rouletteAnimator;

        private int _price;
        
        private void OnEnable()
        {
            _startRoletteButton.onClick.AddListener(StartSpine);
            _collectButton.onClick.AddListener(StartCloseWinningPanel);
        }

        private void OnDisable()
        {
            _startRoletteButton.onClick.RemoveListener(StartSpine);
            _collectButton.onClick.RemoveListener(StartCloseWinningPanel);
        }

        public void ReceiveCard(Card card) => 
            OnReceivedCard?.Invoke(new Card[] { card });

        public void ReceiveCristal() => 
            OnReceivedCristal?.Invoke(Random.Range(1, 6));

        public void ReceiveGold() => 
            OnReceivedGold?.Invoke(Random.Range(1,6));

        public void StartSpine()
        {
            if (FindObjectOfType<CristalWallet>().gameObject.GetComponent<CristalWallet>().AmountMoney >= _spinePrise)
            {
                _price = RandomCell();
                
                _startRoletteButton.interactable = false;
                StartCoroutine(_rouletteAnimator.Spine(_price, _rouletteCells));
                OnBuyRouletteSpin?.Invoke(_spinePrise);
            }
        }
        
        private int RandomCell() => 
            Random.Range(0, _rouletteCells.Length);
    
        private void ReceiveItem(IRoulette rouletteItem)
        {
            var taker = rouletteItem;
            taker.TakeItem();
        }

        private void StartCloseWinningPanel()
        {
            StartCoroutine(_rouletteAnimator.CloseWinningPanel(_startRoletteButton));
            ReceiveItem(_rouletteCells[_price].RouletteItem);
        }
    }
}
