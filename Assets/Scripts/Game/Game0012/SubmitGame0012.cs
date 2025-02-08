using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SubmitGame0012 : MonoBehaviour
{
    public GameObject circuit;

    private float grade = 0f;

    public void OnGameSubmit()
    {
        Debug.Log("Submit Game 0012");

        // Required items
        List<string> requiredItems = new List<string> { "Battery", "LightBulb", "Wire", "CloseSwitch" };
        List<string> incorrectItems = new List<string> { "OpenSwitch", "Book" };

        List<GameObject> activeChildren = GetActiveChildren(circuit);

        Debug.Log(activeChildren.Count);

        if (ValidateItems(activeChildren, requiredItems, incorrectItems))
        {
            Debug.Log("Validate items: true");
            // Check positions
            if (ValidatePositions(activeChildren))
            {
                Debug.Log("Validate position: true");
                grade = 10f; // Perfect grade
            }
        }

        grade = Mathf.Clamp(grade, 0f, 10f);

        ShowProgressDialog(grade);
    }
    private List<GameObject> GetActiveChildren(GameObject parent)
    {
        return parent.transform.Cast<Transform>()
            .Where(child => child.gameObject.activeSelf)
            .Select(child => child.gameObject)
            .ToList();
    }

    private bool ValidateItems(List<GameObject> activeChildren, List<string> requiredItems, List<string> incorrectItems)
    {
        // Get the names of active children
        List<string> activeNames = activeChildren.Select(child => child.name).ToList();

        // Check for extra or missing items
        if (!requiredItems.All(item => activeNames.Contains(item))) return false;
        if (activeNames.Any(item => incorrectItems.Contains(item))) return false;

        return true; // All required items are present, and no incorrect items are present
    }

    private bool ValidatePositions(List<GameObject> activeChildren)
    {
        // Define position combinations
        var positionCombo1 = new List<CircuitItem>
        {
            new CircuitItem{
            positionType = "Position1",
            positionVector = new Vector3(-1.99f, 1.9f, -17.15f),
            rotationVector = Quaternion.identity,
            itemName =  "LightBulb"
            },new CircuitItem{
            positionType = "Position4",
            positionVector = new Vector3(-44.40002f, 15.59998f, 39.5f),
            rotationVector = Quaternion.Euler(0f, 90f, 0f),
            itemName =  "Battery"
            },new CircuitItem{
            positionType = "Position2",
            positionVector = new Vector3(0f, 0f, 0f),
            rotationVector = Quaternion.identity,
            itemName =  "CloseSwitch"
            },new CircuitItem{
            positionType = "Position3",
            positionVector = new Vector3(-0.5f, 1.9f, 9.9f),
            rotationVector = Quaternion.Euler(0f, -89.451f, 0f),
            itemName =  "Wire"
            },
            //new CircuitItem("Position1", new Vector3(-1.99f, 1.9f, -17.15f), Quaternion.identity, "LightBulb"),
            //new CircuitItem("Position4", new Vector3(-44.40002f, 15.59998f, 39.5f), Quaternion.Euler(0f, 90f, 0f), "Battery"),
            //new CircuitItem("Position2", new Vector3(0f, 0f, 0f), Quaternion.identity, "CloseSwitch"),
            //new CircuitItem("Position3", new Vector3(-0.5f, 1.9f, 9.9f), Quaternion.Euler(0f, -89.451f, 0f), "Wire"),
        };

        var positionCombo2 = new List<CircuitItem>
        {
            new CircuitItem{
            positionType = "Position4",
            positionVector = new Vector3(0.39f, 1.9f, 36.3f),
            rotationVector = Quaternion.identity,
            itemName =  "LightBulb"
            },new CircuitItem{
            positionType = "Position1",
            positionVector = new Vector3(44.4f, 15.59998f, -20.3f),
            rotationVector = Quaternion.Euler(0f, -90f, 0f),
            itemName =  "Battery"
            },new CircuitItem{
            positionType = "Position3",
            positionVector = new Vector3(-1.12f, 0f, 19.2f),
            rotationVector = Quaternion.Euler(0f, 178.5f, 0f),
            itemName =  "CloseSwitch"
            },new CircuitItem{
            positionType = "Position2",
            positionVector = new Vector3(-0.18f, 1.9f, 9.1f),
            rotationVector = Quaternion.Euler(0f, 89.451f, 0f),
            itemName =  "Wire"
            },
            //new CircuitItem("Position1", new Vector3(44.4f, 15.59998f, -20.3f), Quaternion.Euler(0f, -90f, 0f), "Battery"),
            //new CircuitItem("Position2", new Vector3(-0.18f, 1.9f, 9.1f), Quaternion.Euler(0f, 89.451f, 0f), "Wire"),
            //new CircuitItem("Position3", new Vector3(-1.12f, 0f, 19.2f), Quaternion.Euler(0f, 178.5f, 0f), "CloseSwitch"),
            //new CircuitItem("Position4", new Vector3(0.39f, 1.9f, 36.3f), Quaternion.identity, "LightBulb"),
        };

        // Check if items match any valid position combination
        return MatchesPositionCombo(activeChildren, positionCombo1) || MatchesPositionCombo(activeChildren, positionCombo2);
    }

    private bool MatchesPositionCombo(List<GameObject> activeChildren, List<CircuitItem> positionCombo)
    {
        foreach (var position in positionCombo)
        {
            // Find the corresponding item in active children
            var item = activeChildren.FirstOrDefault(child => child.name == position.itemName);
            Debug.Log($"circuit item: {item}");
            if (item == null) return false; // Item not found

            // Get position from child TextMeshPro
            TextMeshProUGUI positionText = item.transform.Find("Position").GetComponent<TextMeshProUGUI>();
            if (positionText.text != position.positionType) return false;

            // Check position and rotation
            //if (Vector3.Distance(item.transform.position, position.positionVector) > 0.1f) return false;
            //if (Quaternion.Angle(item.transform.rotation, position.rotationVector) > 1f) return false;
        }

        return true; // All items match the position combo
    }


    private void ShowProgressDialog(float correctItemsCount)
    {
        Transform dialogPanel = null;

        if (correctItemsCount >= 5)
        {
            dialogPanel = gameObject.transform.Find("SuccessPanel");
        }
        else
        {
            dialogPanel = gameObject.transform.Find("GameOverPanel");
        }
        Debug.Log("Submit");
        Debug.Log(correctItemsCount);

        if (dialogPanel != null)
        {
            SubmitExercise submitExercise = FindObjectOfType<SubmitExercise>();
            submitExercise.HandleGradeAndPoints(dialogPanel, correctItemsCount);
        }
    }
}
