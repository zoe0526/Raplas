using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButton : DataLoader
{
    public static SkillButton Instance;
    [SerializeField]
    public Button[] mSkillButtonArr;
    [SerializeField]
    public Image[] mSkillCoolImageArr;
    [SerializeField]
    public TextMeshProUGUI[] mSkillCoolTextArr; 
    [SerializeField]
    public SkillInfo[] mSkillDataArr;
 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        } 
        LoadJsonData(out mSkillDataArr, "JsonFiles/Skill");
    } 

    public void ShowCoolTime(int skillnum,float cooltimebase, float cooltimecurr)
    { 
        mSkillCoolImageArr[skillnum].fillAmount = cooltimecurr / cooltimebase;

        float min = cooltimecurr / 60;
        float sec = cooltimecurr % 60;
        mSkillCoolTextArr[skillnum].text = string.Format("{0}", sec.ToString("00")); 
    }

    public void ShowSkillCoolTime(int skillnum,bool visible)
    {
        mSkillCoolImageArr[skillnum].gameObject.SetActive(visible);
    }
}

[System.Serializable]
public class SkillInfo
{
    public int ID;
    public string Name;
    public string Content;

    public float Cooltime;
    public float CooltimeCurr;

    public int AttackValue;
    public string Anim;
} 