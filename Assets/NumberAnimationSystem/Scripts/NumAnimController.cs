using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;

public class NumAnimController : MonoBehaviour
{
    public TMP_Text _numberText;
    public int m_currentNum;

    void Start()
    {
    }

    public void Animate(NumAnimData d)
    {
        // Stop previous animation
        LeanTween.cancel(gameObject);
        LeanTween.value(gameObject, m_currentNum, d._total, d._animationTime)
            .setEase(LeanTweenType.easeOutQuart)
            .setOnUpdate(UpdateUI);
    }

    public void UpdateUI(float v)
    {
        m_currentNum = (int)v;
        _numberText.text = v.ToString("0");
    }
}