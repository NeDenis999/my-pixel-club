using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Page : MonoBehaviour
{
    [SerializeField] 
    private CanvasGroup _canvasGroup;
    
    private Vector3 _startPosition;
    private Sequence _sequence;


    private void Start()
    {
        _startPosition = transform.localPosition;
    }

    public void Show()
    {
        _sequence = DOTween.Sequence();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        _sequence.Kill();
        gameObject.SetActive(false);
    }

    public void StartShowSmooth()
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

    private IEnumerator ShowSmooth()
    {
        _canvasGroup.alpha = 0;
        transform.localPosition = _startPosition + new Vector3(200, 0, 0);
        _sequence
            .Insert(0, DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1, 0.75f))
            .Insert(0, transform.DOLocalMove(_startPosition, 0.75f));
        
        yield return new WaitForSeconds(1);
    }

    private IEnumerator HideSmooth()
    {
        print("HideSmooth");
        _canvasGroup.alpha = 1;
        transform.localPosition = _startPosition;
        DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0, 1);
        transform.DOLocalMove(_startPosition + new Vector3(200, 0, 0), 1);
        yield return new WaitForSeconds(1);
    }
}