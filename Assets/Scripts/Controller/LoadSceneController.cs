using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LoadSceneController : DontDestroy<LoadSceneController>
{
    public enum eScene
    {
        Title,
        GamePad,
        Game,
        Fight
    }

    private string mCurrScene;
    public eScene mScene { get; set; }
    private eScene mSceneLoadState;
    private AsyncOperation mAsynicOper;

    protected override void OnStart()
    {
        base.OnStart();
        mCurrScene = string.Empty;
        mScene = eScene.Title;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAsync(string sceneName,eScene state)
    {
        if(!string.IsNullOrEmpty(mCurrScene))
        {
            return;
        }
        mCurrScene = sceneName;
        mSceneLoadState = state;
        mAsynicOper = SceneManager.LoadSceneAsync(sceneName);
    }

    public void LoadSceneMerge(string sceneName,eScene state)
    {
        if(!string.IsNullOrEmpty(mCurrScene))
        {
            return;
        }
        mCurrScene = sceneName;
        mSceneLoadState = state;
        mAsynicOper = SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
    }

    public eScene CurrentScene()
    {
        return mSceneLoadState;
    }

    void Update()
    {
        if(mAsynicOper!=null&&!string.IsNullOrEmpty(mCurrScene))
        {
            if(mAsynicOper.isDone)
            {
                mCurrScene = string.Empty;
                mScene = mSceneLoadState;
            }
        }
    }
}
