using DG.Tweening;

namespace Pages.Farm
{
    public class FarmPage : Page
    {
        public override void Hide()
        {
            Sequence.Kill();
            transform.localPosition = StartPosition;
            CanvasGroup.alpha = 0;
        }
    }
}