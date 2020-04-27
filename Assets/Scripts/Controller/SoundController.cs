using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum eSoundType
{
    BGM,
    Effect
}
public enum eBGMsound
{
    Title,
    Game,
    Fight
}
public enum eEffectsound
{
    Button,
    Attack,
    Damage,
    LevelUp,
    Bought,
    BossAttack1,
    BossAttack2,
    BossAttack3,
    BossDead 
}
 
public class SoundController : MonoBehaviour
{
    public static SoundController Instance;

    [SerializeField]
    public AudioClip[] mBGMClip;
    [SerializeField]
    public AudioClip[] mEffectClip; 
    private AudioSource[] mAudiosource;

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
        mAudiosource = new AudioSource[2];
    }
    // Start is called before the first frame update
    void Start()
    {
        mAudiosource[(int)eSoundType.BGM] = gameObject.AddComponent<AudioSource>() as AudioSource;
        mAudiosource[(int)eSoundType.BGM].loop = true;
        mAudiosource[(int)eSoundType.BGM].playOnAwake = false;
        mAudiosource[(int)eSoundType.BGM].rolloffMode = AudioRolloffMode.Linear;

        mAudiosource[(int)eSoundType.Effect] = gameObject.AddComponent<AudioSource>() as AudioSource;
        mAudiosource[(int)eSoundType.Effect].loop = false;
        mAudiosource[(int)eSoundType.Effect].playOnAwake = false;
        mAudiosource[(int)eSoundType.Effect].rolloffMode = AudioRolloffMode.Linear; 
    } 

    public void SetBGMVolume(float volume)
    {
        mAudiosource[(int)eSoundType.BGM].volume = volume;
    }

    public void SetEffectVolume(float volume)
    {
        mAudiosource[(int)eSoundType.Effect].volume = volume;
    }

     public void SetBGMMute(bool selected)
    { 
        selected=!selected;
        if(selected)
        {
            mAudiosource[(int)eSoundType.BGM].mute = true; 
        }
        if(!selected)
        {
            mAudiosource[(int)eSoundType.BGM].mute = false;
        }
    }

    public void SetEFFCTMute(bool selected)
    {
        selected =! selected;
        if(selected)
        {
            mAudiosource[(int)eSoundType.Effect].mute = true; 
        }
        if(!selected)
        {
            mAudiosource[(int)eSoundType.Effect].mute = false;
        }
    }
    
    public void PlayBGM(int bgmnum)
    {
        mAudiosource[(int)eSoundType.BGM].clip = mBGMClip[bgmnum];
        mAudiosource[(int)eSoundType.BGM].Play();
    }

    public void PlayEffect(int effectnum)
    { 
        mAudiosource[(int)eSoundType.Effect].clip = mEffectClip[effectnum];
        mAudiosource[(int)eSoundType.Effect].Play();    
    } 
}
