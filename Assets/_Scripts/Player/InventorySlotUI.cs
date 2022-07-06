using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public InventorySlot data;
    public Image image;
    public TMP_Text tmpText;
    public Outline outline;
    public bool isSelected;

    public event Action Selected = delegate { };

    public void UpdateSlot(InventorySlot slotData)
    {
        this.data = slotData;
        if (slotData.IsNothing())
        {
            image.enabled = false;
            image.sprite = null;
            tmpText.text = "";
        }
        else
        {
            image.sprite = slotData.item.Image;
            image.enabled = true;
            tmpText.text = slotData.amount > 1 ? slotData.amount.ToString() : "";
        }
    }

    public void Select(bool select)
    {
        if (select)
            Selected.Invoke();
        isSelected = select;
        outline.enabled = select;
    }
}