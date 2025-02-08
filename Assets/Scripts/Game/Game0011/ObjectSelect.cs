using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ObjectSelect : MonoBehaviour
{
    public TextMeshProUGUI SelectedItem;
    public GameObject InventoryItemsObject;
    private string DefaultColor = "#000000";
    private float DefaultBorderSize = 2f;
    private string SelectColor = "#00F215";
    private float SelectBorderSize = 0.5f;

    public void SelectInventoryItem(GameObject InventoryItem)
    {
        if (InventoryItem == null) return;

        UnityEngine.UI.Image border = InventoryItem.transform.Find("Panel").GetComponent<UnityEngine.UI.Image>();
        
        if(InventoryItem.name == SelectedItem.text)
        {
            SetDefaultBorder(border);

            SelectedItem.text = "";
        }
        else
        {
            GameObject selectedInvItem = InventoryItemsObject.transform.Find(SelectedItem.text).gameObject;

            if (selectedInvItem != null && SelectedItem.text != "")
            {
                UnityEngine.UI.Image borderInvItem = selectedInvItem.transform.Find("Panel").GetComponent<UnityEngine.UI.Image>();

                if (borderInvItem != null)
                SetDefaultBorder(borderInvItem);

            }

            UnityEngine.ColorUtility.TryParseHtmlString(SelectColor, out Color NewBorderColor);
            border.color = NewBorderColor;
            border.pixelsPerUnitMultiplier = SelectBorderSize;
            SelectedItem.text = InventoryItem.name;
        }
    }

    private void SetDefaultBorder(UnityEngine.UI.Image item)
    {
        UnityEngine.ColorUtility.TryParseHtmlString(DefaultColor, out Color DefaultBorderColor);
        item.color = DefaultBorderColor;
        item.pixelsPerUnitMultiplier = DefaultBorderSize;
    }

    public void SelectBoard(GameObject boardItemContainer)
    {
        Debug.Log("boardItemContainer");
        GameObject correctItems = boardItemContainer.transform.Find("CorrectItems").gameObject;
        GameObject otherItems = boardItemContainer.transform.Find("OtherItems").gameObject;

        GameObject correctItemsChoice = correctItems.transform.Find(SelectedItem.text).gameObject;
        GameObject otherItemsChoice = otherItems.transform.Find(SelectedItem.text).gameObject;

        if (SelectedItem.text != "" && (correctItemsChoice!=null || otherItemsChoice!=null))
        {
            correctItemsChoice.SetActive(true);
            otherItemsChoice.SetActive(true);
        }
    }

    }
