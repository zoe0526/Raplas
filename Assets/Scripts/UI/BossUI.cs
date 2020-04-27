using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BossUI : MonoBehaviour
{
    public static BossUI Instance;
    [SerializeField]
    public GameObject mWinPopup;
    [SerializeField]
    public TextMeshProUGUI mGold, mEXP, mLV,mWeapon;
    [SerializeField]
    public TextMeshProUGUI mWinText;
    [SerializeField]
    public Image mWeaponIcon;
    [SerializeField]
    public Item[] mDropItem;
    public int rand; 
    [SerializeField]
    public GaugeBar mHPBar;
    [SerializeField]
    public Image mPreventClick;
     
    void Awake()
    {
        if(Instance==null)
        {
            Instance = this; 
        }
        else
        {
            Destroy(gameObject);
        } 
    } 

    public void ShowBossHPBar(float currHP, float maxHP)
    {
        if(currHP>=40)
        {
            mHPBar.mGaugeBar.color = new Color(0255,255,0);
        }
        if(currHP<40 && currHP>=15)
        {
            mHPBar.mGaugeBar.color = new Color(248, 144, 25);

        }
        if(currHP<15)
        {
            mHPBar.mGaugeBar.color = new Color(255, 0, 0);
        }


        float progress = (float)(currHP / maxHP);
        string progressString = string.Format("{0}/{1}", currHP, maxHP);
        mHPBar.ShowGaugeBar(progress, progressString);
    }
    
    public void ShowValues()
    { 
        mGold.text = string.Format("Coin : {0}",(PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].Gold-PlayerManager.Instance.mStartCoin).ToString());
        mEXP.text = string.Format("EXP : {0}" ,(PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].CurrEXP - PlayerManager.Instance.mStartEXP).ToString());
        mLV.text = string.Format("LV{0} -> LV{1}", PlayerManager.Instance.mStartLV.ToString(), PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].Level.ToString());
        rand= Random.Range(0, mDropItem.Length);
        mWeapon.text = mDropItem[rand].name;
        mWeaponIcon.sprite = mDropItem[rand].icon; 
    } 
}
