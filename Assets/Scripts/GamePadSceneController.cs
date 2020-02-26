using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadSceneController : MonoBehaviour
{
    private void DelayStartLoadGameScene()
    {
        if(LoadSceneController.Instance!=null)
        {
            LoadSceneController.Instance.LoadSceneMerge("Game", LoadSceneController.eScene.Game);

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Invoke("DelayStartLoadGameScene",0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
