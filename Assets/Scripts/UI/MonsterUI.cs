using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterUI : MonoBehaviour
{ 
    [SerializeField]
    public GaugeBar mHPBar; 
    
    public void ShowMonsterHPBar(float currHP, float maxHP)
    {
        float progress = (float)(currHP / maxHP);
        string progressString = string.Format("{0}/{1}", currHP, maxHP);
        mHPBar.ShowGaugeBar(progress, progressString);
    } 
}
