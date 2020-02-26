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
    // Start is called before the first frame update
    void Start()
    {
        
    }
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
        mSkillCoolTextArr[skillnum].text = string.Format("{0} : {1}", min.ToString("00"), sec.ToString("00"));

    }
    
    public void ShowSkillCoolTime(int skillnum,bool visible)
    {
        mSkillCoolImageArr[skillnum].gameObject.SetActive(visible);
    }

    void Update()
    {
       
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

    public int MPcost;
    public string Anim;
}
public enum eSkillAnim
{
    Skill1,
    Skill2,
    Skill3
}
