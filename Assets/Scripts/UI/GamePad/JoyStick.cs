using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler
{ 
    private Image BGImage;
    [SerializeField]
    public Image JoystickImage;
    private Vector3 InputVector;

    void Start()
    {
        BGImage = GetComponent<Image>();
        InputVector = Vector3.zero;
    }

    public virtual void OnDrag(PointerEventData pad)
    {
        Vector2 pos=Vector2.zero;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle
            (BGImage.rectTransform,pad.position,pad.pressEventCamera,out pos))
        {
            pos.x = (pos.x / BGImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / BGImage.rectTransform.sizeDelta.y);
         
            InputVector =new Vector3(pos.x*2 , 0, pos.y * 2 );
            InputVector = (InputVector.magnitude > 1.0f) ? InputVector.normalized : InputVector;
     
            JoystickImage.rectTransform.anchoredPosition = 
                new Vector3(InputVector.x * (BGImage.rectTransform.sizeDelta.x /3),
                InputVector.z * (BGImage.rectTransform.sizeDelta.y/3));
        }
    }

    public virtual void OnPointerDown(PointerEventData pad)
    {
        OnDrag(pad);
    }

    public virtual void OnPointerUp(PointerEventData pad)
    {
        InputVector = Vector3.zero;
        JoystickImage.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float GetHorizontalValue()
    {
        if(InputVector.x!=0)
        {
            return InputVector.x;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }

    public float GetVerticalValue()
    {
        if (InputVector.z != 0)
        {
            return InputVector.z;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    } 
}
