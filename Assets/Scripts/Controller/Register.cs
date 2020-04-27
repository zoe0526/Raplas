using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Register : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI mIDText,mPopText;
    public TMP_InputField mPassWordText;
    [SerializeField]
    public GameObject mIDPopUp;
    private bool mIDRegister;
    [SerializeField]
    public Button mGameStartButton;

    [SerializeField]
    public TextMeshProUGUI mLogInIDText;
    public TMP_InputField mLogInPassWordText;
     
    void Start()
    {
        mIDRegister = false;
    }

    bool MultipleID = false;
    public void CheckID()
    {
        for (int i=0; i<PlayerManager.Instance.mPlayerInfo.Length;i++)
        {
            if(mIDText.text.ToString()==PlayerManager.Instance.mPlayerInfo[i].PlayerID)
            {
                MultipleID = true;
                mPopText.text = "사용불가!중복되는 아이디입니다.";
                mIDPopUp.SetActive(true);
                
                mIDRegister = false;
            }
        }
        if(mIDText.text.Length<6)
        {
            mPopText.text = "5문자 이상의 아이디를 입력해주세요";
            mIDPopUp.SetActive(true);
            mIDRegister = false;
        }
        if(mIDText.text.Length>=6 && !MultipleID)
        {
            mPopText.text = "사용가능한 아이디입니다!";
            mIDPopUp.SetActive(true);
            mIDRegister = true;
        }
        MultipleID = false;
    }

    bool ISID = false;
    bool ISPassword = false;
    public void CheckLogin()
    { 
        for (int i = 0; i <=PlayerManager.Instance.ID; i++)
        { 
            if(mLogInIDText.text.ToString()==PlayerManager.Instance.mPlayerInfo[i].PlayerID)
            {
                ISID = true;
                if(mLogInPassWordText.text.ToString()==PlayerManager.Instance.mPlayerInfo[i].PassWord)
                { 
                    ISPassword = true;
                    PlayerManager.Instance.CurrID = i;
                    mPopText.text = string.Format("{0}님 환영합니다!게임을 시작합니다", mLogInIDText.text);
                    mIDPopUp.SetActive(true);
                    mIDPopUp.GetComponentInChildren<Button>().gameObject.SetActive(false);
                    mGameStartButton.gameObject.SetActive(true);
                    mGameStartButton.onClick.AddListener(() => { TitleController.Instance.OnLoadScene(); }); 
                }
               if(!ISPassword)
                {
                    mPopText.text = "패스워드가 틀렸습니다!";
                    mIDPopUp.SetActive(true);
                }
                break;
            }
            if(!ISID)
            {
                mPopText.text = "존재하지 않는 아이디입니다!";
                mIDPopUp.SetActive(true);
            }
        }
        ISID = false;
        ISPassword = false; 
    }
     
    int Charnum=0;
    int intnum = 0;

    private void PasswordCheck()
    {
        if(mPassWordText.text.Length<8 )
        {
            mPopText.text = "비밀번호는 8자이상 , 문자와 숫자를 섞어서 입력해주세요";
            mIDPopUp.SetActive(true);
        }
        
        for(int i=0; i<mPassWordText.text.Length;i++)
        {
            if (mPassWordText.text[i] <= '9' && mPassWordText.text[i] >= '0')
                intnum++;
            if (mPassWordText.text[i] <= 'z' && mPassWordText.text[i] >= 'a')
                Charnum++;
            if (mPassWordText.text[i] <= 'Z' && mPassWordText.text[i] >= 'A')
                Charnum++;
        }
        if(intnum==0 || Charnum==0)
        {

            mPopText.text = "비밀번호는 8자이상 , 문자와 숫자를 섞어서 입력해주세요";
            mIDPopUp.SetActive(true);
        }
    }

    public void CloseIDPopUp()
    {
        mIDPopUp.SetActive(false); 
    }
    public void RegisterClicked()
    {
        PasswordCheck();
        if(mIDRegister)
        {
            if(intnum>0 && Charnum>0 && intnum+Charnum>=8)
            { 
                mPopText.text = string.Format("가입완료!{0}님 환영합니다!게임을 시작합니다", mIDText.text);

                PlayerManager.Instance.LoadDatas(); 
                PlayerManager.Instance.CurrID = PlayerManager.Instance.ID; 
                PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.ID].PlayerID = mIDText.text.ToString(); 
                PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.ID].PassWord = mPassWordText.text.ToString(); 
                PlayerManager.Instance.SaveDatas();

                mIDPopUp.SetActive(true);
                mIDPopUp.GetComponentInChildren<Button>().gameObject.SetActive(false);
                mGameStartButton.gameObject.SetActive(true);
                mGameStartButton.onClick.AddListener(() => { TitleController.Instance.OnLoadScene(); }); 
            } 
        }
        if(!mIDRegister)
        {
            mPopText.text = "아이디 검사부터 완료해주세요";
            mIDPopUp.SetActive(true);
        }
        intnum = 0;
        Charnum = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (mIDText.text.Length < 5)
            mIDRegister = false; 
    }
}
