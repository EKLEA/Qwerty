using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICraftSlot : UIInventorySlot
{
    [SerializeField] GameObject locker;
    [SerializeField] GameObject nameOfItem;

    private void LateUpdate()
    {
        Refresh();
        if (slot == null || slot.item == null)
        {
            gameObject.GetComponent<Image>().enabled = false;
            _uiInventoryItem.gameObject.SetActive(false);
            nameOfItem.SetActive(false);
            locker.SetActive(true);
            locker.GetComponentInChildren<TextMeshProUGUI>().text = $"нету";
        }
        else if (slot.item.info.requielevel > PlayerController.Instance.playerLevelList.levelTier)
        {
            gameObject.GetComponent<Image>().enabled = false;
            _uiInventoryItem.gameObject.SetActive(false);
            nameOfItem.SetActive(false);
            locker.SetActive(true); 
            locker.GetComponentInChildren<TextMeshProUGUI>().text = $"Нужна карта расширения {slot.item.info.requielevel} уровня";
        }
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
