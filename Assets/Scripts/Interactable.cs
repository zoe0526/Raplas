using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;//플레이어가 인터렉트할수잇는 범위
    
    bool isFocus = false;
    bool hasinteracted = false;
    public Transform mInteractPos;//인터렉트 물체에서 인터렉트할 위치
    private Transform mPlayerPos;

    public virtual void Interact()
    {
        Debug.Log("Interacting with" + transform.name);
    }//인터렉트하는 오브젝트마다 가지는 속성이 다르므로 overwrite가능하게

    public void OnFocused(Transform position)
    {
        isFocus = true;
        mPlayerPos= position;
        hasinteracted = false;
    }
    public void OnDeFocused()
    {
        isFocus = false;
        mPlayerPos = null;
        hasinteracted = false;
    }

    void OnDrawGizmosSelected()
    {
        if(mInteractPos==null)
        {
            mInteractPos = transform;
        }//인터렉트 포지션이 따로없을때는 그냥 물체의 위치로 대신함
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(mInteractPos.position, radius);
    }

    void Update()
    {
        if(isFocus && !hasinteracted)
        {
            float dis = Vector3.Distance(mPlayerPos.position, mInteractPos.position);
            if(dis<=radius)
            {
                Interact();
                hasinteracted = true;
            }
        }
    } 
}
