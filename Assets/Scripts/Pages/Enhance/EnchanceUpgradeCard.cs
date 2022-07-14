using Pages.Enhance.Card_Statistic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Pages.Enhance
{
    public class EnchanceUpgradeCard : MonoBehaviour
    {    
        [SerializeField] private EnchanceCardCollection _enhanceCardCollection;
        [SerializeField] private Enchance _enhance;

        [SerializeField] private Image _UIIcon;
        [SerializeField] private Image _frameImage;
        [SerializeField] private Sprite _standardSprite;

        private EnhanceCardForUpgradeStatistic _cardStatistic;

        private CardCollectionCell _cardCell;
        public CardCollectionCell CardCell => _cardCell;

        [SerializeField] private Button _resetButton;

        private void Start()
        {
            _cardStatistic = FindObjectOfType<EnhanceCardForUpgradeStatistic>().gameObject.GetComponent<EnhanceCardForUpgradeStatistic>();
        }

        private void OnEnable()
        {
            _cardCell = null;
            _UIIcon.sprite = _standardSprite;
            _frameImage.gameObject.SetActive(false);
            _resetButton.onClick.AddListener(Reset);
            GetComponent<Button>().onClick.AddListener(OpenCardCollection);

        }

        private void OnDisable()
        {
            _resetButton.onClick.RemoveListener(Reset);
            GetComponent<Button>().onClick.RemoveListener(OpenCardCollection);
        }

        public void SetCardForUpgrade(CardCollectionCell card)
        {
            if (card == null) throw new System.ArgumentNullException();

            _cardCell = card;
            _UIIcon.sprite = CardCell.Icon.sprite;
            _frameImage.gameObject.SetActive(true);
            _frameImage.sprite = CardCell.Card.GetFrame();
            _cardStatistic.Render(this);
        
            print(card.LevelPoint);
        }


        private void OpenCardCollection()
        {
            _enhanceCardCollection.gameObject.SetActive(true);
            _enhanceCardCollection.UpgradeCard = this;
        }

        private void Reset()
        {
            _cardCell = null;
            _UIIcon.sprite = _standardSprite;
            _frameImage.gameObject.SetActive(false);
            _enhance.gameObject.SetActive(false);
            _enhance.gameObject.SetActive(true);
        }
    }
}
