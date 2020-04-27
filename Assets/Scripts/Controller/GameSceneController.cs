using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    public static GameSceneController Instance;
   
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

    void Start()
    {
        PlayerUI.Instance.mPlayerID.text = PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].PlayerID.ToString();
        PlayerUI.Instance.mPlayerID2.text = PlayerUI.Instance.mPlayerID.text;
    }


    public void OnLoadScene()
    {
        LoadSceneController.Instance.LoadSceneAsync("GamePad", LoadSceneController.eScene.Fight);
 
        SoundController.Instance.PlayBGM((int)eBGMsound.Fight);
        PlayerManager.Instance.Loaded++;
        if(Dialog.Instance.num==5)
        { 
            PlayerManager.Instance.DialogLoaded = true;
        }
        PlayerManager.Instance.mStartCoin = PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].Gold;
        PlayerManager.Instance.mStartEXP = PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].CurrEXP;
        PlayerManager.Instance.mStartLV = PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].Level;
        
    } 
}
