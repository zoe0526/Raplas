using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MonsterGenerator : DataLoader
{
    public static MonsterGenerator Instance;

    [SerializeField]
    public GameObject[] mMonsterPrefab;
   
    public MonsterInfo[] mMonsterInfo; 
    [SerializeField]
    public Transform[] mMonsterGeneratePos;
    private Vector3[] mRandPos;
     
    [SerializeField]
    public Transform mBossPos;
    [SerializeField]
    public GameObject mBossPrefab;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        LoadJsonData(out mMonsterInfo,"JsonFiles/Monster"); 
        
        for (int i = 0; i < mMonsterInfo.Length; i++)
        {
            mMonsterInfo[i].Prefab = mMonsterPrefab[i];
            mMonsterInfo[i].Prefab.transform.position = Vector3.zero;
        }
    }
    // Start is called before the first frame update
    void Start()
    { 
        Generate(0);
        Generate(1);
        Generate(2);
        GameObject boss = Instantiate(mBossPrefab,mBossPos) as GameObject;
        boss.GetComponent<NavMeshAgent>().Warp(boss.transform.position);
    }

    void Generate(int i)
    {
        int num = 3; 
        while(true)
        {
            int rand = Random.Range(0, mMonsterInfo.Length);
            float Randx = Random.Range(-1f, 1f);
            float Randz = Random.Range(-1f, 1f);
            
            mMonsterInfo[rand].Prefab.transform.position += new Vector3(Randx, 0, Randz); 
            GameObject obj = Instantiate(mMonsterInfo[rand].Prefab, mMonsterGeneratePos[i]) as GameObject;
            obj.GetComponent<NavMeshAgent>().Warp(obj.transform.position);
            
            num--;
            if (num == 0)
                break;
        } 
    } 
}
