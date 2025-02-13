using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandleExerciseSelection : MonoBehaviour
{
    public Material selectedMaterial;
    public Material emptyMaterial;
    public TextMeshProUGUI selectedItems;

    public void SelectCircuit(GameObject selection)
    {
        TextMeshProUGUI number = selection.transform.Find("Number").GetComponent<TextMeshProUGUI>();
        GameObject checkIcon = selection.transform.Find("Check").gameObject;
        GameObject planeBox = selection.transform.Find("Plane").gameObject;
        Renderer renderer = planeBox.GetComponent<Renderer>();

        if (checkIcon.activeSelf)
        {
            checkIcon.SetActive(false);
            renderer.material = emptyMaterial;
        }
        else
        {
            checkIcon.SetActive(true);
            renderer.material = selectedMaterial;
        }

        AddNumberToFinalItems(number.text);
    }

    private void AddNumberToFinalItems(string numberSelected)
    {
        string allItems = selectedItems.text;

        if (allItems.Contains(numberSelected))
        {
            // Remove the number from the string
            allItems = allItems.Replace(numberSelected, "");
        }
        else
        {
            // Append the number to the string
            allItems += numberSelected;
        }

        selectedItems.text = allItems;
        Debug.Log($"Updated numString: {allItems}");
    }
}
