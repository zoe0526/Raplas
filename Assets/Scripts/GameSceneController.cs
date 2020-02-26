using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OnLoadScene();
    }

    public void OnLoadScene()
    {
        LoadSceneController.Instance.LoadSceneAsync("GamePad", LoadSceneController.eScene.Game);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
