using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool<T> : MonoBehaviour where T : Component
{
    [SerializeField]
    public T[] mOrignArr;
    private List<T>[] mPool; 

    protected void PoolSetUp()
    {
        mPool = new List<T>[mOrignArr.Length];
        for (int i = 0; i < mPool.Length; i++)
        {
            mPool[i] = new List<T>();
        }
    }

    private void Start()
    { 
        PoolSetUp();
    }

    public T GetFromPool(int id)
    {
        for (int i = 0; i < mPool[id].Count; i++)
        { 
            if (!mPool[id][i].gameObject.activeInHierarchy)
            {
                mPool[id][i].gameObject.SetActive(true);
                return mPool[id][i];
            } 
        }
        return GetNewOBJ(id);
    }

    protected virtual T GetNewOBJ(int id)
    {
        T newOBJ = Instantiate(mOrignArr[id]);
        mPool[id].Add(newOBJ);
        return newOBJ;
    } 
}
