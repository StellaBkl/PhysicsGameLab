using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectPoint : MonoBehaviour
{
    public TextMeshProUGUI selectedItems;
    public TextMeshProUGUI answerItems;

    public void OnPointClick(string currentItemName)
    {
        // Get the last letter of the name
        if (string.IsNullOrEmpty(currentItemName)) return;
        char lastLetter = currentItemName[currentItemName.Length - 1];
        Debug.Log($"names {lastLetter} {currentItemName}");

        // Process the target string
        ModifyString(lastLetter);
    }

    // Function to add or remove the letter in the targetString
    private void ModifyString(char letter)
    {
        // Split the string into an array of individual letters
        var letterArray = selectedItems.text.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

        // Check if the letter exists in the array
        string letterString = letter.ToString();
        if (System.Array.Exists(letterArray, l => l == letterString))
        {
            // If it exists, remove it
            selectedItems.text = string.Join(",", System.Array.FindAll(letterArray, l => l != letterString));
        }
        else
        {
            // If it does not exist, add it
            if (!string.IsNullOrEmpty(selectedItems.text)) selectedItems.text += ",";
            selectedItems.text += letter;
        }

        answerItems.text = selectedItems.text;

        // Debug to see the updated string
        Debug.Log("Updated String: " + selectedItems.text);
    }
}
