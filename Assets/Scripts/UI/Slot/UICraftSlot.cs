using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICraftSlot : UISlotWithLock
{
    
    [SerializeField] GameObject nameOfItem;

    
    protected override void SetLock()
    {
        
        if (slot == null || slot.item == null)
        {
            gameObject.GetComponent<Image>().enabled = false;
            _uiInventoryItem.gameObject.SetActive(false);
            nameOfItem.SetActive(false);
            locker.SetActive(true);
            locker.GetComponentInChildren<TextMeshProUGUI>().text = $"����";
        }
        else
        {
            if (slot != null && slot.item != null)
            {
                gameObject.GetComponent<Image>().enabled = false;
                _uiInventoryItem.gameObject.SetActive(false);
                nameOfItem.SetActive(false);
                locker.SetActive(true);

                if (slot.item.info.requielevel > PlayerController.Instance.playerLevelList.levelTier)
                {
                    locker.GetComponentInChildren<TextMeshProUGUI>().text = $"����� ����� ���������� {slot.item.info.requielevel} ������";
                    slot.isBlock = true;
                }
                else if (PlayerInventory.Instance.equippedItems.GetAllItems(slot.item.info.id).Length > 0 || PlayerInventory.Instance.storageItems.GetAllItems(slot.item.info.id).Length > 0)
                {
                    slot.isBlock = true;
                    locker.GetComponentInChildren<TextMeshProUGUI>().text = $"������� ��� ���� � ���������";
                }
                else
                {
                    slot.isBlock = false;
                    gameObject.GetComponent<Image>().enabled = true;
                    _uiInventoryItem.gameObject.SetActive(true);
                    nameOfItem.SetActive(true);
                    locker.SetActive(false);
                    nameOfItem.GetComponentInChildren<TextMeshProUGUI>().text = slot.item.info.title;
                }
            }
        }
    }
    
}
