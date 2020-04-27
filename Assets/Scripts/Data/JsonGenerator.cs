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
    public MonsterInfo[] mMonsterInfoArr;

    [SerializeField]
    public PlayerInfo[] mPlayerInfoArr; 

    void Start()
    { 
        mSkillInfoArr = new SkillInfo[3];
        mMonsterInfoArr = new MonsterInfo[3];
        mPlayerInfoArr = new PlayerInfo[20];
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

    public void GenerateMonster()
    {
        string data = JsonConvert.SerializeObject(mMonsterInfoArr, Formatting.Indented);
        Debug.Log(data);
        StreamWriter writer=new StreamWriter(Application.dataPath + "/Resources/JsonFiles/Monster.json");
        writer.Write(data);
        writer.Close();
    }
    public void LoadMonster()
    {
        string data = Resources.Load<TextAsset>("JsonFiles/Monster").text;
        mMonsterInfoArr = JsonConvert.DeserializeObject<MonsterInfo[]>(data);
        Debug.Log(mMonsterInfoArr);

    }
    public void GeneratePlayer()
    {
        string data = JsonConvert.SerializeObject(mPlayerInfoArr, Formatting.Indented);
        Debug.Log(data);
        StreamWriter writer = new StreamWriter(Application.dataPath + "/Resources/JsonFiles/PlayerInfo.json");
        writer.Write(data);
        writer.Close();

    }
    public void LoadPlayer()
    { 
        string data = Resources.Load<TextAsset>("JsonFiles/PlayerInfo").text;
        mPlayerInfoArr = JsonConvert.DeserializeObject<PlayerInfo[]>(data);
        Debug.Log(mPlayerInfoArr);
    } 

}
