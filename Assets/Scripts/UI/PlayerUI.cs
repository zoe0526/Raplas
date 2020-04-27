using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance;
    [SerializeField]
    public GaugeBar mHPBar;
    [SerializeField]
    public TextMeshProUGUI mGoldText;
    [SerializeField]
    public GaugeBar mEXPBar;
    [SerializeField]
    public TextMeshProUGUI mLevelText,mLevelText2;
    [SerializeField]
    public TextMeshProUGUI mPlayerID,mPlayerID2;

    [SerializeField]
    public TextMeshProUGUI mAttackstat, mSheildstat, mHPstat;
    [SerializeField]
    public GameObject mLosePopup;
    [SerializeField]
    public GameObject mPreventClick;
    [SerializeField]
    public TextMeshProUGUI mCountdown;

    void Awake()
    { 
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    } 

    public void ShowGold(float gold)
    {
        mGoldText.text = gold.ToString();
    }

    public void ShowLevel(int level)
    {
        string str= string.Format("LV.{0}", level);
        mLevelText.text = str;
        mLevelText2.text = level.ToString();
    } 

    public void ShowPlayerHPBar(float currHP,float maxHP)
    {
        float progress = (float)(currHP / maxHP);
        string progressString = string.Format("{0}/{1}", currHP, maxHP);
        mHPBar.ShowGaugeBar(progress, progressString);
    }

    public void ShowPlayerEXPBar(float currEXP,float maxEXP)
    {
        float progress = (float)(currEXP / maxEXP);
        string progressstring = string.Format("{0}/{1}", currEXP, maxEXP);
        mEXPBar.ShowGaugeBar(progress, progressstring);
    }

}
