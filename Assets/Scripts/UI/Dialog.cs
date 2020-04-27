using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Dialog : MonoBehaviour
{
    public static Dialog Instance; 
    [SerializeField]
    public Image mDialog;
    [SerializeField]
    public TextMeshProUGUI mText;
    [SerializeField]
    public Button mNo, mYes;
    public int num = 0;
  
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

    public bool ShowDialog()
    {
        mDialog.gameObject.SetActive(true);
        Text(0); 
        return true;
    }

    public bool ShowDialog2()
    {
        mDialog.gameObject.SetActive(true);
        Text(5);
        return true;
    } 

    public void Text(int i)
    {
        switch(i)
        {
            case 0:
                string str0 = string.Format("안녕하세요 {0}님!! \n 제 이름은 모나 입니다. \n 마을 촌장을 맡고 있죠.",
                    PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].PlayerID);
                mText.text = str0; 

                break;
            case 1:
                string str1 = string.Format("요즘 몬스터들이 늘어나서 마을사람들이 고통받고있어요..\n혹시 도와주실수 있을까요?");
                mNo.gameObject.SetActive(true);
                mYes.gameObject.SetActive(true);
                mText.text = str1;
                
                break;
            case 2:
                mNo.gameObject.SetActive(false);
                mYes.gameObject.SetActive(false);
                string str2 = string.Format("감사합니다!! \n 여기 작은 성의입니다.\n 이걸로 상점에서 장비를 구입하세요.");
                mText.text = str2;
                PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].Gold += 100;

                break;
            case 3: 
                mNo.gameObject.SetActive(false);
                mYes.gameObject.SetActive(false);
                string str3 = string.Format("아쉽군요.\n 준비가 되시면 다시 말 걸어주세요.");
                mText.text = str3;

                break;
            case 4: 
                mDialog.gameObject.SetActive(false);

                break;
            case 5:
                string str6 = string.Format("안녕하세요.{0}님", PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].PlayerID);
                mText.text = str6;

                break;
            case 6:
                mDialog.gameObject.SetActive(false);

                break; 
        } 
    }
    private bool isDone;
    public void ButtonClickNo()
    { 
        Text(3);
        num = 3; 
        isDone = false; 
    }
    public void ButtonClickYes()
    {
        Text(2);
        num = 3;
        isDone = true; 
    } 
    void Start()
    {
        mNo.onClick.AddListener(() =>
        {
            ButtonClickNo();
        });

        mYes.onClick.AddListener(() => {

            ButtonClickYes();
        });
    } 

    void Update()
    {
            if (Input.GetMouseButtonDown(0) && num != 1 && mDialog.gameObject.activeInHierarchy)
            {
                num++;
                Text(num);
            } 
            if(!PlayerManager.Instance.DialogLoaded)
            { 
                if (num >= 4)
                {
                    if (!isDone)
                    {
                        num = 0;
                    }
                    else
                    {
                        num = 5;
                    }
                }
            }
        if (PlayerManager.Instance.DialogLoaded)
            num = 5; 
    }
}