using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerManager : DataLoader
{
    public static PlayerManager Instance;
    [Header("Player Info")]
    [SerializeField]
    public GameObject mPlayer;
    public PlayerInfo[] mPlayerInfo;
    public int ID;
    public int CurrID=0;
    public int Loaded = 0;
    public float mStartCoin,mStartEXP,mStartLV; 
    public bool DialogLoaded;
    public bool mLevelup;

    [SerializeField]
    public Transform mLevelupPos;
    
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
        LoadDatas();
     
        for(int i=0; i<mPlayerInfo.Length;i++)
        {
            if(mPlayerInfo[i].PlayerID=="")
            {
                ID = i;
                break;
            } 
        }
        mPlayerInfo[ID].ID = ID;
        mPlayerInfo[ID].Gold = 0;
        mPlayerInfo[ID].Level = 1;
        mPlayerInfo[ID].CurrEXP = 0;
        mPlayerInfo[ID].MaxEXP = 5;
        mPlayerInfo[ID].MaxLife = 30;

        SaveDatas();
        if (mPlayerInfo[CurrID].MaxLife != 30)
            DialogLoaded = true;
    }
    public void LoadDatas()
    {
        LoadJsonData(out mPlayerInfo, "JsonFiles/PlayerInfo");
    }

    public void SaveDatas()
    {
        SaveData(mPlayerInfo, "/Resources/JsonFiles/PlayerInfo.json");
    }

      void Start()
    {
        mLevelup = false;  
        PlayerUI.Instance.ShowGold(mPlayerInfo[CurrID].Gold);
    }
 
    public void EXPCalculate()
    {
        if (mPlayerInfo[CurrID].CurrEXP > mPlayerInfo[CurrID].MaxEXP)
        {
            float temp = 0;
            temp = mPlayerInfo[CurrID].CurrEXP;
            mPlayerInfo[CurrID].CurrEXP = temp- mPlayerInfo[CurrID].MaxEXP;
            mPlayerInfo[CurrID].MaxEXP += 50;
            mPlayerInfo[CurrID].Level++;
            mPlayerInfo[CurrID].MaxLife +=3;
            mLevelup = true;
        }
    }

    public void EndGame()
    {
        /*
        mPlayerInfo[CurrID].Gold = mPlayerInfo[CurrID].Gold;
        mPlayerInfo[CurrID].Level = mPlayerInfo[CurrID].Level;
        mPlayerInfo[CurrID].MaxEXP = mPlayerInfo[CurrID].MaxEXP;
        mPlayerInfo[CurrID].MaxLife = mPlayerInfo[CurrID].MaxLife;
        mPlayerInfo[CurrID].CurrEXP = mPlayerInfo[CurrID].CurrEXP;
         */
        SaveDatas();


#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://google.com");"
#else
        Application.Quit();
#endif
    }

    void Update()
    {
        PlayerUI.Instance.ShowLevel(mPlayerInfo[CurrID].Level);
        PlayerUI.Instance.ShowPlayerEXPBar(mPlayerInfo[CurrID].CurrEXP, mPlayerInfo[CurrID].MaxEXP);
        PlayerUI.Instance.ShowGold(mPlayerInfo[CurrID].Gold);
    }
}
[System.Serializable]
public class PlayerInfo
{
    public int ID;
    public float CurrEXP;
    public float MaxEXP;
    public float Gold;
    public int Level;
    public string PlayerID;
    public string PassWord;
    public float MaxLife;
 
}