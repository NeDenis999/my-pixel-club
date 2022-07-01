using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Decks.CardCell
{
    public class CardDisplay : MonoBehaviour
    {
        [SerializeField] 
        private Image _icon;

        [SerializeField] 
        private Image _frame;
        
        private Sprite[] _frames;

        [Inject]
        private void Construct(AssetProviderService assetProviderService)
        {
            _frames = assetProviderService.Frames;
        }
        
        public void UpdateDisplay(Card card)
        {
            _icon.sprite = card.UIIcon;

            if (card.Name != "Empty")
            {
                _frame.sprite = card.GetFrame(_frames);
                _frame.color = Color.white;
            }
            else
            {
                _frame.color = Color.clear;
            }
        }
    }
}