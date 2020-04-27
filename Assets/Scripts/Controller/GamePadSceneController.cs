using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadSceneController : MonoBehaviour
{
    private void DelayStartLoadGameScene()
    { 
        if(LoadSceneController.Instance!=null &&LoadSceneController.Instance.mScene==LoadSceneController.eScene.Game)
        {
            LoadSceneController.Instance.LoadSceneMerge("Game", LoadSceneController.eScene.Game);
        }
        else if(LoadSceneController.Instance != null && LoadSceneController.Instance.mScene ==LoadSceneController.eScene.Fight)
        {
            LoadSceneController.Instance.LoadSceneMerge("Fight", LoadSceneController.eScene.Fight);
        } 
    } 

    void Start()
    {
        Invoke("DelayStartLoadGameScene",0.1f);
    } 
}
