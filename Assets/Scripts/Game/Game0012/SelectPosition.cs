using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SelectPosition : MonoBehaviour
{
    public TextMeshProUGUI SelectedItem;
    public TextMeshProUGUI previousPosition;
    public GameObject GameTemplate;
    public GameObject Inventory;

    private List<CircuitItem> ItemPositions = new List<CircuitItem>();
    private string currentObject="";
    private string BackgroundDefaultColor = "#FFFFFF";
    private string BackgroundSelectColor = "#4B8C46";

    void Start()
    {

        //Position 1
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position1",
            positionVector = new Vector3(-1.99f, 1.9f, -17.15f),
            isCorrectPosition = true,
            itemName = "LightBulb"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position1",
            positionVector = new Vector3(44.4f, 15.59998f, -20.3f),
            rotationVector = Quaternion.Euler(0f, -90f, 0f),
            isCorrectPosition = true,
            itemName = "Battery"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position1",
            positionVector = new Vector3(-8.7f, 0f, 7.1f),
            rotationVector = Quaternion.Euler(0f, 90f, 0f),
            isCorrectPosition = false,
            itemName = "CloseSwitch"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position1",
            positionVector = new Vector3(-0.6f, 1.9f, 4.8f),
            rotationVector = Quaternion.Euler(0f, 180f, 0f),
            isCorrectPosition = false,
            itemName = "Wire"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position1",
            positionVector = new Vector3(-8.7f, 0f, 7.1f),
            rotationVector = Quaternion.Euler(0f, 90f, 0f),
            isCorrectPosition = false,
            itemName = "OpenSwitch"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position1",
            positionVector = new Vector3(0.3f, 8.55f, -17.3f),
            isCorrectPosition = false,
            itemName = "Book"
        });

        //Position 2
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position2",
            positionVector = new Vector3(26.4f, 1.9f, 3.7f),
            isCorrectPosition = false,
            itemName = "LightBulb"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position2",
            positionVector = new Vector3(22.5f, 15.59998f, -31.2f),
            rotationVector = Quaternion.Euler(0f, 0f, 0f),
            isCorrectPosition = false,
            itemName = "Battery"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position2",
            positionVector = new Vector3(0f, 0f, 0f),
            rotationVector = Quaternion.Euler(0f, 0f, 0f),
            isCorrectPosition = true,
            itemName = "CloseSwitch"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position2",
            positionVector = new Vector3(-0.18f, 1.9f, 9.1f),
            rotationVector = Quaternion.Euler(0f, 89.451f, 0f),
            isCorrectPosition = true,
            itemName = "Wire"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position2",
            positionVector = new Vector3(0f, 0f, 0f),
            rotationVector = Quaternion.Euler(0f, 0f, 0f),
            isCorrectPosition = false,
            itemName = "OpenSwitch"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position2",
            positionVector = new Vector3(26.2f, 8.55f, 12.6f),
            isCorrectPosition = false,
            itemName = "Book"
        });

        //Position 3
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position3",
            positionVector = new Vector3(-25.9f, 1.9f, 8.9f),
            isCorrectPosition = false,
            itemName = "LightBulb"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position3",
            positionVector = new Vector3(-27.5f, 15.59998f, -31.2f),
            rotationVector = Quaternion.Euler(0f, 0f, 0f),
            isCorrectPosition = false,
            itemName = "Battery"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position3",
            positionVector = new Vector3(-1.12f, 0f, 19.2f),
            rotationVector = Quaternion.Euler(0f, 178.5f, 0f),
            isCorrectPosition = true,
            itemName = "CloseSwitch"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position3",
            positionVector = new Vector3(-0.5f, 1.9f, 9.9f),
            rotationVector = Quaternion.Euler(0f, -89.451f, 0f),
            isCorrectPosition = true,
            itemName = "Wire"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position3",
            positionVector = new Vector3(-1.12f, 0f, 19.2f),
            rotationVector = Quaternion.Euler(0f, 178.5f, 0f),
            isCorrectPosition = false,
            itemName = "OpenSwitch"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position3",
            positionVector = new Vector3(-23.5f, 8.55f, 9.5f),
            isCorrectPosition = false,
            itemName = "Book"
        });

        //Position 4
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position4",
            positionVector = new Vector3(0.39f, 1.9f, 36.3f),
            isCorrectPosition = true,
            itemName = "LightBulb"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position4",
            positionVector = new Vector3(-44.40002f, 15.59998f, 39.5f),
            rotationVector = Quaternion.Euler(0f, 90f, 0f),
            isCorrectPosition = true,
            itemName = "Battery"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position4",
            positionVector = new Vector3(12.3f, 0f, 13.4f),
            rotationVector = Quaternion.Euler(0f, -90f, 0f),
            isCorrectPosition = false,
            itemName = "CloseSwitch"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position4",
            positionVector = new Vector3(-0.6f, 1.9f, 15.8f),
            rotationVector = Quaternion.Euler(0f, 0f, 0f),
            isCorrectPosition = false,
            itemName = "Wire"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position4",
            positionVector = new Vector3(12.3f, 0f, 13.4f),
            rotationVector = Quaternion.Euler(0f, -90f, 0f),
            isCorrectPosition = false,
            itemName = "OpenSwitch"
        });
        ItemPositions.Add(new CircuitItem
        {
            positionType = "Position4",
            positionVector = new Vector3(1.6f, 8.55f, 37.7f),
            isCorrectPosition = false,
            itemName = "Book"
        });
    }

    void OnMouseDown()
    {
        Transform circuitItem = GameTemplate.transform.Find("Circuit/"+ SelectedItem.text);
        Transform currentObjectItem = GameTemplate.transform.Find("Circuit/"+ currentObject);

        if (circuitItem != null)
        {
            Debug.Log("Test Position"+ gameObject.name);
            Debug.Log(currentObject);

            if (currentObjectItem!=null && currentObject!="")
            {
                TextMeshProUGUI currentObjectP = currentObjectItem.gameObject.transform.Find("Position").GetComponent<TextMeshProUGUI>();
                Debug.Log(currentObjectP.text);
                currentObject = !currentObjectP.text.Equals("") && !currentObjectP.text.Equals(gameObject.name) ? "": currentObject;
                currentObject = currentObjectP.text.Equals("") && !currentObject.Equals(SelectedItem.text) ? "": currentObject;
            }
            TextMeshProUGUI itemPosition = circuitItem.gameObject.transform.Find("Position").GetComponent<TextMeshProUGUI>();

            Debug.Log("currentObject");
            Debug.Log(currentObject);
            if (currentObject.Equals("") || currentObject.Equals(SelectedItem.text))
            {
                foreach (var item in ItemPositions)
                {
                    Debug.Log("ITEM POSITION"+ item.positionVector);
                    if (item.positionType == gameObject.name && item.itemName.Equals(SelectedItem.text))
                    {
                        circuitItem.localPosition = item.positionVector;
                        circuitItem.localRotation = item.rotationVector;
                        Debug.Log("FOUND POSITION");

                        circuitItem.gameObject.SetActive(previousPosition.text != gameObject.name);
                        itemPosition.text = circuitItem.gameObject.activeSelf ? gameObject.name : "";
                        currentObject = circuitItem.gameObject.activeSelf ? SelectedItem.text : "";

                        UnityEngine.UI.Image imageInv = Inventory.transform.Find(SelectedItem.text).GetComponent<UnityEngine.UI.Image>();
                        if (circuitItem.gameObject.activeSelf)
                        {
                            UnityEngine.ColorUtility.TryParseHtmlString(BackgroundSelectColor, out Color filColor);
                            imageInv.color = filColor;
                        }
                        else
                        {
                            UnityEngine.ColorUtility.TryParseHtmlString(BackgroundDefaultColor, out Color filColor);
                            imageInv.color = filColor;
                        }
                        break;
                    }

                }
                previousPosition.text = previousPosition.text != gameObject.name ? gameObject.name : "";
            }
        }
    }
}
