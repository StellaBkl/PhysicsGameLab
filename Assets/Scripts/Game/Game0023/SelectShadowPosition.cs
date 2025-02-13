using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SelectShadowPosition : MonoBehaviour
{
    public Material selectedMaterial;
    public Material emptyMaterial;

    private GameObject directions;
    private TextMeshProUGUI selectedPoint;

    public void SelectPosition(GameObject currentPosition)
    {
        TextMeshProUGUI number = currentPosition.transform.Find("Number").GetComponent<TextMeshProUGUI>();
        GameObject checkIcon = currentPosition.transform.Find("Check").gameObject;
        GameObject planeBox = currentPosition.transform.Find("Plane").gameObject;
        Renderer renderer = planeBox.GetComponent<Renderer>();
        string currentItem = number.text + "-" + currentPosition.name;

        GameObject selection = gameObject.transform.Find("Selection" + number.text).gameObject;
        selectedPoint = selection.transform.Find("SelectedPoint").GetComponent<TextMeshProUGUI>();
        directions = selection.transform.Find("Directions").gameObject;

        if (selectedPoint.text != currentItem)
        {
            clearSelectedDirections();
        }

        if (checkIcon.activeSelf)
        {
            selectedPoint.text = "";
            checkIcon.SetActive(false);
            renderer.material = emptyMaterial;
        }
        else
        {
            selectedPoint.text = currentItem;
            checkIcon.SetActive(true);
            renderer.material = selectedMaterial;
        }
    }

    private void clearSelectedDirections()
    {
        foreach (Transform direction in directions.transform)
        {
            GameObject checkIcon = direction.Find("Check").gameObject;
            GameObject planeBox = direction.Find("Plane").gameObject;
            Renderer renderer = planeBox.GetComponent<Renderer>();

            checkIcon.SetActive(false);
            renderer.material = emptyMaterial;
        }
        selectedPoint.text = "";
    }
}
