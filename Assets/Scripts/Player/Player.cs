using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
enum ePlayerAnimation
{
    IsIdle,
    IsWalk,
    IsJump,
    IsAttack

}
public class Player : MonoBehaviour
{
    
    private Animator mAnimator;
    private Rigidbody mRB;
    
    
    public Vector3 mMoveVectorP;
    public Vector3 mMoveVectorK;
    public Vector3 mCurrPos;
    float mcountdowntime;
    
    [SerializeField]
    public Button mAttackButton;
    [SerializeField]
    public JoyStick mJoyStick;

    [SerializeField]
    private int mSpeed;
    private int mRunSpeed;
    [SerializeField]
    private PlayerInfo[] mInfo;
    
    public PlayerInfo[] Infos { get { return mInfo; } }
    [SerializeField]
    private SkillInfo[] mSkillInfo;

    public SkillInfo[] SkillInfos { get { return mSkillInfo; } }
    void Awake()
    {
        mRB = GetComponent<Rigidbody>();

    }
    
    private IEnumerator SetCoolTime(int id)
    {
        WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
       // WaitForSeconds fixedUpdate = new WaitForSeconds(1f);
        SkillButton.Instance.mSkillDataArr[id].CooltimeCurr = SkillButton.Instance.mSkillDataArr[id].Cooltime;
        SkillButton.Instance.ShowSkillCoolTime(SkillButton.Instance.mSkillDataArr[id].ID,true);
        while (SkillButton.Instance.mSkillDataArr[id].CooltimeCurr > 0)
        {
            SkillButton.Instance.mSkillDataArr[id].CooltimeCurr -= Time.fixedDeltaTime;
           
            SkillButton.Instance.ShowCoolTime(SkillButton.Instance.mSkillDataArr[id].ID, SkillButton.Instance.mSkillDataArr[id].Cooltime, SkillButton.Instance.mSkillDataArr[id].CooltimeCurr);
            yield return fixedUpdate;

        }
        SkillButton.Instance.ShowSkillCoolTime(SkillButton.Instance.mSkillDataArr[id].ID, false);

    }



    // Use this for initialization
    void Start()
    {
       
        mAnimator = GetComponent<Animator>();
        mMoveVectorP = Vector3.zero;
        mcountdowntime = 3.0f;
        mRunSpeed = mSpeed + 3;

    }
    /*
    IEnumerator CountDown(float value)
    {
        WaitForSeconds sec = new WaitForSeconds(1);
        mCountdowntime = value;
        while (true)
        {
            yield return sec;
            mCountdowntime--;
            if (mCountdowntime <= 0)
                break;
        }
        

    }
    */
    public void MoveWithPad()
    {
        mAnimator.SetBool("IsWalk", false);
        mAnimator.SetBool("IsRun", false);
        
        float horizontal = mJoyStick.GetHorizontalValue();
        float vertical = mJoyStick.GetVerticalValue();
        mMoveVectorP = new Vector3(horizontal, 0, vertical);
        mRB.transform.position += mMoveVectorP.normalized * Time.deltaTime*mSpeed;


        if (mMoveVectorP != Vector3.zero)
        {
            mAnimator.SetBool("IsWalk", true);
           
            Debug.Log(mcountdowntime);
            mcountdowntime -= Time.deltaTime;
            if (mcountdowntime <= 0)
            {
               
                mAnimator.SetBool("IsRun", true);
                mRB.transform.position += mMoveVectorP.normalized * Time.deltaTime * mRunSpeed;
            }
    
        }
        if(mMoveVectorP==Vector3.zero)
        mcountdowntime = 3.0f;
        
        if (vertical == 0 && horizontal == 0)
            return;
        Quaternion rotate = Quaternion.LookRotation(mMoveVectorP);
        mRB.MoveRotation(rotate);

    }
    private void MoveWithKey()
    {
        mAnimator.SetBool("IsWalk", false);
        mAnimator.SetBool("IsRun", false);
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        mMoveVectorK = new Vector3(horizontal, 0, vertical);
        mRB.transform.position += mMoveVectorK.normalized * mSpeed * Time.deltaTime;


        if (mMoveVectorK != Vector3.zero)
        {
            mAnimator.SetBool("IsWalk", true);
           
        }
        if (vertical == 0 && horizontal == 0)
            return;
        Quaternion rotate = Quaternion.LookRotation(mMoveVectorK);
        mRB.MoveRotation(rotate);

    }

    private void Action() //플레이어 액션들
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mAnimator.SetTrigger("IsJump");
        }
        mAttackButton.onClick.AddListener(() => { mAnimator.SetTrigger("IsAttack"); });
      for(int i=0; i<SkillButton.Instance.mSkillDataArr.Length;i++)
        {
            SkillAttack(i);
           
        }
    }
    public void SkillAttack(int id) //스킬공격
    {
        SkillButton.Instance.mSkillButtonArr[id].onClick.AddListener(() =>
        {

            mAnimator.SetTrigger(SkillButton.Instance.mSkillDataArr[id].Anim);
            ActivateSkill(id);
        });
    }
    public void ActivateSkill(int id)
    {
        
        StartCoroutine(SetCoolTime(SkillButton.Instance.mSkillDataArr[id].ID));
    }
    void Update()
    {
        MoveWithPad();
        Action();
    }
}

public class PlayerInfo
{
    public int ID;
    public string Name;
    public string Contents;
    public int Level;
    
    public float CoolTime;
    public float CoolTimeCurr;
    public float Duration;
    

}
