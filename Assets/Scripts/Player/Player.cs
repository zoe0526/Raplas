using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; //UI클릭시 이동안되게 하기위함
using UnityEngine.AI;
 
[RequireComponent(typeof(PlayerNav))]
public class Player : MonoBehaviour
{ 
    [Header("Player Movement")]
    [SerializeField]
    public LayerMask mMovementMask;
    private Rigidbody mRB; 
    private Vector3 mMoveVectorP, mMoveVectorK;
    [SerializeField]
    public int mSpeed;  
    private PlayerNav mNav;

    [Header("Player Contributes")]
    [SerializeField]
    public Button mAttackButton; 
    private Interactable mFocus; 
    public Camera mCam; 
    private EffectPool mEffectPool;
    private DamagePool mDamagePool;
    private CharacStat mStat;
    public bool mPlayerExists; 
    private PlayerStats mPlayerStat;
    private AnimationController mAnim;

    [Header("Monster Attack")]
    public List<GameObject> mTargetList;

    bool isclicked;
    void Awake()
    {
        mRB = GetComponent<Rigidbody>();
        mCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
 
        mNav = GetComponent<PlayerNav>();
        mStat = GetComponent<CharacStat>();
        mPlayerStat = GetComponent<PlayerStats>();
   
        mTargetList = new List<GameObject>();
        mAnim = AnimationController.Instance;
        mEffectPool = GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectPool>();
        mDamagePool = GameObject.FindGameObjectWithTag("Effect").GetComponent<DamagePool>();
        isclicked = false;
    }

    void Start()
    { 
        Action(); //플레이어 액션들
        
        EquipManager.Instance.mSwordTargetMesh = GameObject.FindGameObjectWithTag("RightHand").GetComponent<SkinnedMeshRenderer>();
        EquipManager.Instance.mHelmetTargetMesh = GameObject.FindGameObjectWithTag("Head").GetComponent<SkinnedMeshRenderer>();
      
        if(!mPlayerExists && PlayerManager.Instance.Loaded==0)
        {
            mPlayerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
       
        else
        {
            Destroy(gameObject);
        }
        
    }
    
    public void Event_Attack()
    {
        SoundController.Instance.PlayEffect((int)eEffectsound.Attack);
        for (int i=0; i<mTargetList.Count;i++)
        {
            GameObject target = mTargetList[i];
            if(target.CompareTag("Monster"))
            {
                transform.LookAt(target.transform);
                MonsterStats monster = target.GetComponent<MonsterStats>();
                monster.mAnimator.SetTrigger(eMonsterState.IsDamage.ToString());

                if (gameObject.GetComponent<PlayerStats>().mDamage.GetValue() != 0)
                {
                    Effect effect = mEffectPool.GetFromPool((int)eEffectType.PlayerAttack);
                    effect.transform.position = monster.transform.position;

                    DamageValueEffect val = mDamagePool.GetFromPool((int)eDamageEffect.Damageval);
                    val.mDamagevalue.color = new Color(255, 255, 0);
                    val.mDamagevalue.fontSize = 20;
                    val.mDamagevalue.text = (-gameObject.GetComponent<PlayerStats>().mDamage.GetValue()).ToString();
                    val.transform.position = monster.GetComponentInChildren<Canvas>().transform.position;
                }

                Attack(monster);

                if (monster.IsDead)
                {
                    mTargetList.Remove(monster.gameObject); 
                }
            }
            if(target.CompareTag("Boss"))
            {
                transform.LookAt(target.transform);
                Boss boss = target.GetComponent<Boss>();
               //boss.mAnimator.SetTrigger(eBossState.IsDamage.ToString());
                
                if (gameObject.GetComponent<PlayerStats>().mDamage.GetValue() != 0)
                {
                    Effect effect = mEffectPool.GetFromPool((int)eEffectType.PlayerAttack);
                    effect.transform.position = boss.transform.position;

                    DamageValueEffect val = mDamagePool.GetFromPool((int)eDamageEffect.Damageval);
                    val.mDamagevalue.fontSize = 35;
                    val.mDamagevalue.color = new Color(255, 100, 0);
                    val.mDamagevalue.text = (-gameObject.GetComponent<PlayerStats>().mDamage.GetValue()).ToString();
                    val.transform.position =boss.GetComponentInChildren<Canvas>().transform.position;
                }

                AttackBoss(boss);

                if (boss.IsDead)
                {
                    mTargetList.Remove(boss.gameObject); 
                }
            }
             
        }
    }

    public void Event_SkillAttack(int skillnum)
    {
        SoundController.Instance.PlayEffect((int)eEffectsound.Attack);
        for (int i = 0; i < mTargetList.Count; i++)
        {
            GameObject target = mTargetList[i];

            if(target.CompareTag("Monster"))
            {
                transform.LookAt(target.transform);
                MonsterStats monster = target.GetComponent<MonsterStats>();
                monster.mAnimator.SetTrigger(eMonsterState.IsDamage.ToString());
                AnimationController.Instance.SkillAttack(monster, skillnum);
               
                if (gameObject.GetComponent<PlayerStats>().mDamage.GetValue() != 0)
                {
                    Effect effect = mEffectPool.GetFromPool((int)eEffectType.PlayerAttack);
                    effect.transform.position = monster.transform.position;

                    DamageValueEffect val = mDamagePool.GetFromPool((int)eDamageEffect.Damageval);
                    val.mDamagevalue.fontSize = 20;
                    val.mDamagevalue.color = new Color(255, 255, 0);
                    val.mDamagevalue.text = (-SkillButton.Instance.mSkillDataArr[skillnum].AttackValue).ToString();

                    val.transform.position = monster.GetComponentInChildren<Canvas>().transform.position;

                }
                
                Effect effect2 = mEffectPool.GetFromPool((int)eEffectType.Damage);
                effect2.transform.position = transform.position;

                if (monster.IsDead)
                { 
                    mTargetList.Remove(monster.gameObject); 
                }
            }
            if (target.CompareTag("Boss"))
            {
                transform.LookAt(target.transform);
                Boss boss = target.GetComponent<Boss>();
                //boss.mAnimator.SetTrigger(eMonsterState.IsDamage.ToString());
                AnimationController.Instance.SkillAttack(boss, skillnum);

                if (gameObject.GetComponent<PlayerStats>().mDamage.GetValue() != 0)
                {
                    Effect effect = mEffectPool.GetFromPool((int)eEffectType.PlayerAttack);
                    effect.transform.position = boss.transform.position;

                    DamageValueEffect val = mDamagePool.GetFromPool((int)eDamageEffect.Damageval);
                    val.mDamagevalue.fontSize = 35;
                    val.mDamagevalue.color = new Color(255, 100, 0);
                    val.mDamagevalue.text = (-SkillButton.Instance.mSkillDataArr[skillnum].AttackValue).ToString();

                    val.transform.position = boss.GetComponentInChildren<Canvas>().transform.position;

                }

                Effect effect2 = mEffectPool.GetFromPool((int)eEffectType.Damage);
                effect2.transform.position = transform.position;

                if (boss.IsDead)
                { 
                    mTargetList.Remove(boss.gameObject); 
                }
            }
        }
    }

    public void Attack(MonsterStats target)
    {
        target.TakeDamage(mPlayerStat.mDamage.GetValue()); 
        Effect effect = mEffectPool.GetFromPool((int)eEffectType.Damage);
        effect.transform.position = transform.position;
    }

    public void AttackBoss(Boss target)
    {
        target.TakeDamage(mPlayerStat.mDamage.GetValue());
        Effect effect = mEffectPool.GetFromPool((int)eEffectType.Damage);
        effect.transform.position = transform.position;
    }
     
    int preventmultiple = 0;  
    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.CompareTag("Portal"))
        {  
            mNav.mNavagent.enabled = false; //작동 안하는 시점 찾기
            GameSceneController.Instance.OnLoadScene(); 
        }
        if (coll.tag.Equals("Monster"))
        {
            for(int i=0; i<mTargetList.Count;i++)
            {
                if (coll.gameObject == mTargetList[i])
                    preventmultiple++; 
            }
            if (preventmultiple == 0)
                mTargetList.Add(coll.gameObject);
            else
                preventmultiple = 0; 
        }
        if(coll.tag.Equals("Boss"))
        {
            for (int i = 0; i < mTargetList.Count; i++)
            {
                if (coll.gameObject == mTargetList[i])
                    preventmultiple++; 
            }
            if (preventmultiple == 0)
                mTargetList.Add(coll.gameObject);
            else
                preventmultiple = 0; 
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if(coll.tag.Equals("Monster"))
        {
            mTargetList.Remove(coll.gameObject);
        }
        if(coll.tag.Equals("Boss"))
        {
            mTargetList.Remove(coll.gameObject);
        }
    }

    Vector3 mGoto;  
    public void MoveWithClick()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
           isclicked = true;
            mNav.mNavagent.enabled = true; 
            Ray ray = mCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
        
            if (Physics.Raycast(ray, out hit, 100, mMovementMask))
            {
                minteractPos = Vector3.zero;
                mNav.MoveToPoint(hit.point);
                mGoto = hit.point;
                RemoveFocus();
            }
            if (Physics.Raycast(ray, out hit, 100)) //인터렉트 가능한 물체를 만낫을때
            { 
                Interactable interactable = hit.collider.GetComponent<Interactable>(); 
                if (interactable != null)
                {
                    SetFocus(interactable); 
                    minteractPos = interactable.transform.position;
                    mNav.MoveToPoint(minteractPos); 
                    mNav.StopFollowingTarget(); 
                }
            }
        }
        if (isclicked == true)
        {
            mAnim.mAnimator.SetBool(ePlayerAnimation.IsWalk.ToString(), true);
        }
       
        if(Vector3.Distance(new Vector3(mGoto.x, 0, mGoto.z), 
            new Vector3(transform.position.x, 0, transform.position.z))==0)
        {
            isclicked = false;
        }

        if (Vector3.Distance(new Vector3(minteractPos.x, 0, minteractPos.z), 
            new Vector3(transform.position.x, 0, transform.position.z)) == 0)
        {
            isclicked = false;
        }
    }


    private void MoveWithKey()
    {
        mAnim.mAnimator.SetBool(ePlayerAnimation.IsWalk.ToString(), false);
        mAnim.mAnimator.SetBool(ePlayerAnimation.IsRun.ToString(), false);
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        mMoveVectorK = new Vector3(horizontal, 0, vertical);
        mRB.transform.position += mMoveVectorK.normalized * mSpeed * Time.deltaTime;


        if (mMoveVectorK != Vector3.zero)
        { 
            mAnim.mAnimator.SetBool(ePlayerAnimation.IsWalk.ToString(), true);
           
        }
        if (vertical == 0 && horizontal == 0)
            return;
        Quaternion rotate = Quaternion.LookRotation(mMoveVectorK);
        mRB.MoveRotation(rotate);
      
    }
    
    private void Action()   
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mAnim.mAnimator.SetTrigger(ePlayerAnimation.IsJump.ToString());
        }
 
    }
     
    public void MoveWithPad()
    {
        
        float horizontal = GamePadController.Instance.mJoyStick.GetHorizontalValue();
        float vertical = GamePadController.Instance.mJoyStick.GetVerticalValue();
        mMoveVectorP = new Vector3(horizontal, 0, vertical); 
        mRB.transform.position += mMoveVectorP.normalized * Time.deltaTime * mSpeed;
         
        if (mMoveVectorP != Vector3.zero)
        {
            mNav.mNavagent.enabled = false;  
            mAnim.mAnimator.SetBool(ePlayerAnimation.IsWalk.ToString(), true); 
        }
        
        if(mMoveVectorP==Vector3.zero )
        { 
            mAnim.mAnimator.SetBool(ePlayerAnimation.IsWalk.ToString(), false);
        }
        
         transform.LookAt(transform.position + (mMoveVectorP)); 
    }
     
    Vector3 minteractPos;

    void Update()
    {
        MoveWithPad();  
        if (!EventSystem.current.IsPointerOverGameObject())//UI위 마우스 클릭시 케릭터 이동방지
        { 
            MoveWithClick();
        }
        if(!mNav.mNavagent.enabled && mMoveVectorP==Vector3.zero)
        { 
            mAnim.mAnimator.SetBool(ePlayerAnimation.IsWalk.ToString(), false);
        }
         
        if (transform.rotation.x != 0 || transform.rotation.z!=0)
        { 
            transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
        } 

        for (int i = 0; i<mTargetList.Count;i++)
        {
            if (mTargetList[i] == null)
            {
                mTargetList.Remove(mTargetList[i]);
            }
        }

    }

    private void SetFocus(Interactable focus)
    {
        if(focus!=mFocus)
        {
            if(mFocus!=null)
            {
                focus.OnDeFocused();

            }//전 포커스가 잇엇으면 해제해준다.
            
            mFocus = focus;
            mNav.FollowTarget(mFocus);
        }//전 포커스와 겹치지 않게 
       
        focus.OnFocused(transform); 
    }

    private void RemoveFocus()
    {
        if(mFocus!=null)
        {
            mFocus.OnDeFocused(); 
        }
        mFocus = null; 
        mNav.StopFollowingTarget();
    } 
}

