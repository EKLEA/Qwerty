using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class ContextMenu : MonoBehaviour
{

    public GameObject contextMenu => gameObject;
    public Button activeBT;
    public Button equipBT;
    public Button dropBT;
    public UISlot slot;
    public event Action<UISlot> OnActiveBTClickedEvent;
    public event Action<UISlot,bool> OnEquipBTClickedEvent;
    public event Action<UISlot> OnDropBTClickedEvent;
    
    public void SetButtons()
    {
        dropBT.interactable = true;
        activeBT.interactable = false;
        equipBT.interactable = false;
        activeBT.onClick.RemoveListener(Option1Callback);
        equipBT.onClick.RemoveListener(Option2Callback);
        dropBT.onClick.RemoveListener(Option3Callback);
        

        if (slot.GetComponentInParent<UIInventorySlot>().slot.item.info.itemType == ItemTypes.Consumables)
        {
            activeBT.image.color = new Color(61f / 255f, 169f / 255f, 1, 1);
            equipBT.image.color = new Color(61f/255f, 169f/255f, 1, 1);
        }
        else
        {
            activeBT.interactable = true;
            equipBT.interactable = true;
            activeBT.onClick.AddListener(Option1Callback);
            equipBT.onClick.AddListener(Option2Callback);
            if (slot.GetComponentInParent<UIInventorySlot>().slot.item.state.IsEquipped == true)
            {
                equipBT.GetComponentInChildren<TextMeshProUGUI>().text = "—н€ть";
                dropBT.onClick.RemoveListener(Option3Callback);
                dropBT.interactable = false;
            }
            else
            {
                equipBT.GetComponentInChildren<TextMeshProUGUI>().text = "Ёкипировать";
                dropBT.onClick.AddListener(Option3Callback);
                dropBT.interactable = true;
            }
        }
    }
    private void Option1Callback()
    {
        OnActiveBTClickedEvent?.Invoke(slot);
        contextMenu.SetActive(false);
    }

    private void Option2Callback()
    {
        OnEquipBTClickedEvent?.Invoke(slot, !slot.GetComponentInParent<UIInventorySlot>().slot.item.state.IsEquipped);
        contextMenu.SetActive(false);
    }
    private void Option3Callback()
    {
        OnDropBTClickedEvent?.Invoke(slot);
        contextMenu.SetActive(false);
    }
}
