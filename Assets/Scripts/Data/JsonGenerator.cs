using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class JsonGenerator : MonoBehaviour
{
   
    [SerializeField]
    public SkillInfo[] mSkillInfoArr;
    [SerializeField]
    public ItemInfo[] mItemArr;
    // Start is called before the first frame update
    void Start()
    {
      
        mSkillInfoArr = new SkillInfo[3];
        /*
        mItemArr = new ItemInfo[5];
        mItemArr[0] = new ItemInfo();
        mItemArr[0].ItemID = 0;
        mItemArr[0].ItemName = "item";
      
        for(int i=0; i<mItemArr.Length;i++)
        {
            mItemArr[i].ItemIcon = Resources.Load<Sprite>("Item/Dagger");
        }
        mItemArr[0].ItemType = eItemType.Weapon;
        */
        /*
        mSkillInfoArr[0] = new SkillInfo();
        mSkillInfoArr[0].ID = 0;
        mSkillInfoArr[0].Name = "대검 참격";
        mSkillInfoArr[0].Content = "대검으로 충격을 일으켜 목표지역의 적에게 공격력 {0}의 피해를 입힌다.";
        mSkillInfoArr[0].Cooltime = 15;
        mSkillInfoArr[0].CooltimeCurr = 0;
        mSkillInfoArr[0].MPcost = 100;
        mSkillInfoArr[0].Anim = eSkillAnim.Skill1.ToString();
       */
      
    }

    public void GenerateSkill()
    {
        string data = JsonConvert.SerializeObject(mSkillInfoArr, Formatting.Indented);
        Debug.Log(data);
        StreamWriter writer = new StreamWriter(Application.dataPath + "/Resources/JsonFiles/Skill.json");
        writer.Write(data);
        writer.Close();

    }
    public void LoadSkill()
    {
        string data = Resources.Load<TextAsset>("JsonFiles/Skill").text;
        mSkillInfoArr = JsonConvert.DeserializeObject<SkillInfo[]>(data);
        Debug.Log(mSkillInfoArr);
    }

    public void GenerateItem()
    {
        string data = JsonConvert.SerializeObject(mItemArr,Formatting.Indented);
        Debug.Log(data);
        StreamWriter writer = new StreamWriter(Application.dataPath + "/Resources/JsonFiles/Item.json");
        writer.Write(data);
        writer.Close();
    }
    public void LoadItem()
    {
        string data=Resources.Load<TextAsset>("JsonFiles/Item").text;
        mItemArr = JsonConvert.DeserializeObject<ItemInfo[]>(data);
        Debug.Log(mItemArr);
    }

        // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.A))
        {
            GenerateItem();
        }
        */
        if (Input.GetKeyDown(KeyCode.B))
        {
            LoadSkill();
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            LoadItem();
        }
    }
}
