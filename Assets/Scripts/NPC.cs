using UnityEngine;
using UnityEngine.AI;

public class NPC : Interactable
{ 
    public override void Interact()
    {
        base.Interact(); 
        MeetNPC();
    }

    private void MeetNPC()
    { 
        Debug.Log("Interacting with NPC");
       
       if(Dialog.Instance.num==0)
        { 
            bool interacted = Dialog.Instance.ShowDialog(); 
        }
        if (Dialog.Instance.num==5)
        {
            bool ended = Dialog.Instance.ShowDialog2();
        } 
    }
}
