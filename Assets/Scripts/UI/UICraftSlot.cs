using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICraftSlot : UIInventorySlot
{
    [SerializeField] GameObject locker;
    [SerializeField] GameObject nameOfItem;

    private void OnEnable()
    {
        SetLock();
    }
    private void SetLock()
    {
        
        if (slot == null || slot.item == null)
        {
            gameObject.GetComponent<Image>().enabled = false;
            _uiInventoryItem.gameObject.SetActive(false);
            nameOfItem.SetActive(false);
            locker.SetActive(true);
            locker.GetComponentInChildren<TextMeshProUGUI>().text = $"Нету";
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
                    locker.GetComponentInChildren<TextMeshProUGUI>().text = $"Нужна карта расширения {slot.item.info.requielevel} уровня";
                else if (PlayerInventory.Instance.equippedItems.GetAllItems(slot.item.info.id).Length > 0 || PlayerInventory.Instance.storageItems.GetAllItems(slot.item.info.id).Length > 0)
                    locker.GetComponentInChildren<TextMeshProUGUI>().text = $"Предмет уже есть в инвентаре";
                else
                {
                    gameObject.GetComponent<Image>().enabled = true;
                    _uiInventoryItem.gameObject.SetActive(true);
                    nameOfItem.SetActive(true);
                    locker.SetActive(false);
                    nameOfItem.GetComponentInChildren<TextMeshProUGUI>().text = slot.item.info.title;
                }
            }
        }
    }
    public override void Refresh()
    {
        base.Refresh();
        SetLock();
    }
    
}
