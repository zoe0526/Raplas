using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    public static TitleController Instance;
    [SerializeField]
    public Canvas mCanvas;
     
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
    public void OnLoadScene()
    {
        LoadSceneController.Instance.LoadSceneAsync("GamePad",LoadSceneController.eScene.Game);
        SoundController.Instance.PlayBGM((int)eBGMsound.Game);
        mCanvas.gameObject.SetActive(true); 
    }  
}
