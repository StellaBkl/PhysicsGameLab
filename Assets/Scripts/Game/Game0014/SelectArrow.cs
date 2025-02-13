using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SelectArrow : MonoBehaviour
{
    public TextMeshProUGUI SelectedItem;
    public GameObject InventoryItemsObject;
    public GameObject BatteryItem; 
    public Material selectedMaterial;
    public Material emptyMaterial;

    //private string BackgroundDefaultColor = "#FFFFFF";
    //private string BackgroundSelectColor = "#4B8C46";

    void OnMouseDown()
    {
        if (SelectedItem.text == "") return;

        TextMeshProUGUI selectedArrow = BatteryItem.transform.Find("SelectedArrow").GetComponent<TextMeshProUGUI>();
        Renderer renderer = gameObject.GetComponent<Renderer>();

        if (gameObject.name == selectedArrow.text)
        {
            renderer.material = emptyMaterial;
            selectedArrow.text = "";
        }
        else
        {
            SetDefaultMaterials();
            GameObject selectedArrowItem = BatteryItem.transform.Find("Arrows/"+selectedArrow.text).gameObject;

            if (selectedArrowItem != null && selectedArrow.text != "")
            {
                renderer.material = emptyMaterial;
            }

            renderer.material = selectedMaterial;
            selectedArrow.text = gameObject.name;
        }

        //Show plus, minus
        GameObject correctItems = BatteryItem.transform.Find("CorrectItems").gameObject;
        GameObject otherItems = BatteryItem.transform.Find("OtherItems").gameObject;

        DeactivateActiveChildren(correctItems);
        DeactivateActiveChildren(otherItems);

        Debug.Log(selectedArrow.text + "-" + SelectedItem.text);
        Transform correctItemsChoice = correctItems.transform.Find(gameObject.name + "-" + SelectedItem.text);
        Transform otherItemsChoice = otherItems.transform.Find(gameObject.name + "-" + SelectedItem.text);

        if (correctItemsChoice != null)
        {
            correctItemsChoice.gameObject.SetActive(selectedArrow.text != "");
        }
        if (otherItemsChoice != null)
        {
            otherItemsChoice.gameObject.SetActive(selectedArrow.text != "");
        }
    }

    private void DeactivateActiveChildren(GameObject parent)
    {
        // Loop through all child objects of the parent
        foreach (Transform child in parent.transform)
        {
            // Check if the child is active
            if (child.gameObject.activeSelf && child.gameObject.name.StartsWith(gameObject.name))
            {
                // Deactivate the child
                child.gameObject.SetActive(false);
            }
        }
    }

    private void SetDefaultMaterials()
    {
        Renderer rendererL = BatteryItem.transform.Find("Arrows/Left").GetComponent<Renderer>();
        Renderer rendererR = BatteryItem.transform.Find("Arrows/Right").GetComponent<Renderer>();

        rendererL.material = emptyMaterial;
        rendererR.material = emptyMaterial;
    }
}
