using UnityEngine;

public class ItemPickUp : Interactable
{
    public Item mItem;
 
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }
    private void PickUp()
    {
        Debug.Log("Picking up" + mItem.name);
        bool WasPickedUp=Inventory.Instance.Add(mItem);
        if(WasPickedUp)
        {
   
            Destroy(gameObject);
        }
    }

}
