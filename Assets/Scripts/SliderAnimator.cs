using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SliderAnimator : MonoBehaviour
{
    private Sequence _sequence;

    [SerializeField]
    private Slider _slider;

    public void UpdateSlider(float value)
    {
        _sequence?.Kill();
        _sequence = DOTween.Sequence();

        _slider.value = 0;
        _sequence.Insert(0, DOTween.To(()=> _slider.value, x=> _slider.value = x, value, 1)); 
    }
        
    public void UpdateSlider(float value, float maxValue)
    {
        _sequence?.Kill();
        _sequence = DOTween.Sequence();
            
        _slider.value = 0;
        _slider.maxValue = maxValue;
        _sequence.Insert(0, DOTween.To(()=> _slider.value, x=> _slider.value = x, value, 1)); 
    }
        
    public void UpdateSlider(float value, float maxValue, float time)
    {
        _sequence?.Kill();
        _sequence = DOTween.Sequence();
            
        _slider.value = 0;
        _slider.maxValue = maxValue;
        _sequence.Insert(0, DOTween.To(()=> _slider.value, x=> _slider.value = x, value, time)); 
    }

    private void OnApplicationQuit()
    {
        _sequence?.Kill();
    }
}