using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Page : MonoBehaviour
{
    [SerializeField] 
    protected CanvasGroup CanvasGroup;

    protected Vector3 StartPosition;
    protected Sequence Sequence;
    
    public void InitStartPosition() => 
        StartPosition = transform.localPosition;

    public void Show()
    {
        Sequence?.Kill();
        
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        Sequence?.Kill();
        
        transform.localPosition = StartPosition;
        gameObject.SetActive(false);
    }

    public virtual void StartShowSmooth()
    {
        if (gameObject.activeSelf)
            return;
        
        Show();
        StartCoroutine(ShowSmooth());
    }
    
    public void StartHideSmooth()
    {
        StartCoroutine(HideSmooth());
    }

    protected IEnumerator ShowSmooth()
    {
        Sequence?.Kill();
        Sequence = DOTween.Sequence();
        
        CanvasGroup.alpha = 0;
        transform.localPosition = StartPosition + new Vector3(200, 0, 0);
        Sequence
            .Insert(0, DOTween.To(() => CanvasGroup.alpha, x => CanvasGroup.alpha = x, 1, 0.75f))
            .Insert(0, transform.DOLocalMove(StartPosition, 0.75f));
        
        yield return new WaitForSeconds(1);
    }

    private IEnumerator HideSmooth()
    {
        Sequence?.Kill();
        Sequence = DOTween.Sequence();

        CanvasGroup.alpha = 1;
        transform.localPosition = StartPosition;
        DOTween.To(() => CanvasGroup.alpha, x => CanvasGroup.alpha = x, 0, 1);
        transform.DOLocalMove(StartPosition + new Vector3(200, 0, 0), 1);
        yield return new WaitForSeconds(1);
    }
    
    private void OnApplicationQuit() => 
        Sequence?.Kill();
}