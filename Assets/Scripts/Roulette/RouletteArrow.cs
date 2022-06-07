using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RouletteArrow : MonoBehaviour
{
    public event UnityAction<int> OnBuyRouletteSpin;

    [SerializeField] private int _spinePrise;

    [SerializeField] private int _min;
    [SerializeField] private int _max;

    public void StartSpine()
    {
        if (FindObjectOfType<CristalWallet>().gameObject.GetComponent<CristalWallet>().AmountMoney >= _spinePrise)
        {
            OnBuyRouletteSpin?.Invoke(_spinePrise);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(Spine());
        }
    }

    private IEnumerator Spine()
    {
        var finelPoint = Random.Range(_min, _max);;

        while (finelPoint > 0)
        {
            transform.Rotate(0, 0, -(finelPoint * Time.deltaTime));
            finelPoint--;
            yield return new WaitForSeconds(0.01f);
        }

        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RouletteCell>())
            ReceiveItem(other.GetComponent<RouletteCell>().RouletteItem);
    }

    private void ReceiveItem(IRoulette rouletteItem)
    {
        var taker = rouletteItem;
        taker.TakeItem();
    }
}
