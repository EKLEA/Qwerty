using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem :  UIItem
{

    [SerializeField] private Image _imageIcon;
    [SerializeField] private Text _textAmount;
    public IItem item { get; private set; }
    
    public void Refresh (IInventorySlot slot)
    {
        if (slot.isEmpty)
        {
            Cleanup();
            return;
        }

        item = slot.item;
        _imageIcon.color = Color.white;
        _imageIcon.sprite = item.info.spriteIcon;
        _imageIcon.gameObject.SetActive(true);

        var textAmountEnabled = slot.count > 1;
        _textAmount.gameObject.SetActive(textAmountEnabled);
        if (textAmountEnabled)
            _textAmount.text = $"x{slot.count.ToString()}";
    }
   
    private void Cleanup()
    {
        _textAmount.gameObject.SetActive(false);
        _imageIcon.gameObject.SetActive(false);
    }

}
