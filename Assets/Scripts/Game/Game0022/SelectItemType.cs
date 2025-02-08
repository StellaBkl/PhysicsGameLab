using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemType : MonoBehaviour
{
    public TextMeshProUGUI SelectedItems;
    public GameObject InventoryPanel;

    public void OnItemTypeClick(Toggle currentToggle)
    {
        if (SelectedItems.text == "") return;

        TextMeshProUGUI answare = InventoryPanel.transform.Find(SelectedItems.text+ "/Answer").GetComponent<TextMeshProUGUI>();

        if (answare != null)
        {
            answare.text = currentToggle.isOn? currentToggle.name:"";
        }
    }
}
