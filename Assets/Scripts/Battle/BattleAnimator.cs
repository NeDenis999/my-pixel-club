using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class BattleAnimator : MonoBehaviour
    {
        [SerializeField] 
        private HorizontalLayoutGroup _horizontalLayoutGroup;
        
        public IEnumerator AppearanceCards()
        {
            _horizontalLayoutGroup.padding = new RectOffset(0, 0, 0, -800);
            yield return new WaitForSeconds(1);
            SpreadCards();
            yield return new WaitForSeconds(1);
        }

        private void SpreadCards()
        {
            //DOTween.To(()=>x, )
        }
    }
}