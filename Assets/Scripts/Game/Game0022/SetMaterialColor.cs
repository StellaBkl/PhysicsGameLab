using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetMaterialColor : MonoBehaviour
{
    public TextMeshProUGUI SelectedItems;
    public GameObject InventoryPanel;
    public GameObject SelectType;
    public string MaterialPath;

    public void OnInventoryClick()
    {
        if (SelectedItems.text == "")
        { 
            gameObject.SetActive(false);
            SelectType.SetActive(false);
            return;
        }
        InitializeItemType();
        gameObject.SetActive(true);
        SelectType.SetActive(true);
        
        Renderer renderer = gameObject.GetComponent<Renderer>();
        Material newMaterial = Resources.Load<Material>(MaterialPath + SelectedItems.text);

        if (renderer != null && newMaterial != null)
        {
            renderer.material = newMaterial;
        }
    }

    private void InitializeItemType()
    {
        Transform item = InventoryPanel.transform.Find(SelectedItems.text);
        if (item != null)
        {
            TextMeshProUGUI answer = item.transform.Find("Answer").GetComponent<TextMeshProUGUI>();
            if (answer.text == "") 
            {
                foreach (Toggle toggle in SelectType.transform.Find("ItemType").GetComponentsInChildren<Toggle>())
                {
                    toggle.isOn = false;
                }
            }
            else
            {
                Toggle selectedToggle = SelectType.transform.Find("ItemType/" + answer.text).GetComponent<Toggle>();
                if (selectedToggle != null)
                {
                    selectedToggle.isOn = true;
                }
            }
        }

    }
}
