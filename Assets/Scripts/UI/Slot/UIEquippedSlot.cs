using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquippedSlot : UISlotWithLock
{
    protected override void SetLock()
    {
        if(slot.requieType==ItemTypes.UsableItem)
            locker.SetActive(false);
        if (slot.requieType == ItemTypes.Perks)
        {
                locker.SetActive(slot.isBlock);
        }

        if (slot.requieType== ItemTypes.Ability)
        {
            if (slot.item.info.id == "sideSpell") 
            {   
                if (!PlayerController.Instance.playerLevelList.SideCast)
                    locker.SetActive(true);
                else locker.SetActive(false); 
            }



            if (slot.item.info.id == "downSpell") 
            {
                if (!PlayerController.Instance.playerLevelList.DownCast)
                    locker.SetActive(true);
                else locker.SetActive(false);
            }


            if (slot.item.info.id == "upSpell")
            {
                if (!PlayerController.Instance.playerLevelList.UPCast)
                    locker.SetActive(true);
                else locker.SetActive(false);
            }
        }

    }
    public override void Refresh()
    {
        base.Refresh();
        SetLock();
    }
}
