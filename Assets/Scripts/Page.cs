using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Page : MonoBehaviour
{
    [SerializeField] 
    protected CanvasGroup CanvasGroup;

    [SerializeField] 
    private bool _isZeroPosition;
    
    protected Vector3 StartPosition;
    protected Sequence Sequence;


    private void Start()
    {
        InitStartPosition();
    }

    private void InitStartPosition() => 
        StartPosition = _isZeroPosition ? Vector3.zero : transform.localPosition;

    public void Show()
    {
        Sequence = DOTween.Sequence();
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        Sequence.Kill();
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
        CanvasGroup.alpha = 0;
        transform.localPosition = StartPosition + new Vector3(200, 0, 0);
        Sequence
            .Insert(0, DOTween.To(() => CanvasGroup.alpha, x => CanvasGroup.alpha = x, 1, 0.75f))
            .Insert(0, transform.DOLocalMove(StartPosition, 0.75f));
        
        yield return new WaitForSeconds(1);
    }

    private IEnumerator HideSmooth()
    {
        print("HideSmooth");
        CanvasGroup.alpha = 1;
        transform.localPosition = StartPosition;
        DOTween.To(() => CanvasGroup.alpha, x => CanvasGroup.alpha = x, 0, 1);
        transform.DOLocalMove(StartPosition + new Vector3(200, 0, 0), 1);
        yield return new WaitForSeconds(1);
    }
}