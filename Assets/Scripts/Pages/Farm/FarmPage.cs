using DG.Tweening;

namespace FarmPage.Farm
{
    public class FarmPage : Page
    {
        public override void Hide()
        {
            Sequence.Kill();
            transform.localPosition = StartPosition;
            CanvasGroup.alpha = 0;
        }

        public override void StartShowSmooth()
        {
            Show();
            StartCoroutine(ShowSmooth());
        }
    }
}