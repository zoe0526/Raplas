using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GaugeBar : MonoBehaviour
{ 
    [SerializeField]
    public Image mGaugeBar;
    [SerializeField]
    public Text mGaugebarText;

    public void ShowGaugeBar(float progress, string text)
    {
        mGaugeBar.fillAmount = progress;
        mGaugebarText.text = text;
    }
}
