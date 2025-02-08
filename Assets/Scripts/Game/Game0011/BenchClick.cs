using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class BenchClick : MonoBehaviour
{

    public TextMeshProUGUI SelectedItem;
    public GameObject CurrentBoardItemContainer;
    public GameObject OtherBoardItemContainer;
    public GameObject Inventory;

    private string BackgroundDefaultColor = "#FFFFFF";
    private string BackgroundSelectColor = "#4B8C46";


    void OnMouseDown()
    {
        GetItemChoice(CurrentBoardItemContainer, true);
        GetItemChoice(OtherBoardItemContainer, false);
    }

    private void GetItemChoice(GameObject boardItemContainer, bool isCurrent)
    {
        GameObject correctItems = boardItemContainer.transform.Find("CorrectItems").gameObject;
        GameObject otherItems = boardItemContainer.transform.Find("OtherItems").gameObject;

        Transform correctItemsChoice = correctItems.transform.Find(SelectedItem.text);
        GameObject itemsChoiceObj = correctItemsChoice != null ? correctItemsChoice.gameObject : null;

        Transform otherItemsChoice = otherItems.transform.Find(SelectedItem.text);
        itemsChoiceObj = itemsChoiceObj != null ? itemsChoiceObj : (otherItemsChoice != null ? otherItemsChoice.gameObject : null);

        Debug.Log("itemsChoiceObj: " + itemsChoiceObj);

        if (SelectedItem.text != "")
        {
            if (itemsChoiceObj != null)
            {
                if(isCurrent)
                {
                    itemsChoiceObj.SetActive(!itemsChoiceObj.activeSelf);

                    UnityEngine.UI.Image item = Inventory.transform.Find(SelectedItem.text).GetComponent<UnityEngine.UI.Image>();
                    if (itemsChoiceObj.activeSelf)
                    {
                        UnityEngine.ColorUtility.TryParseHtmlString(BackgroundSelectColor, out Color filColor);
                        item.color = filColor;
                    }
                    else
                    {
                        UnityEngine.ColorUtility.TryParseHtmlString(BackgroundDefaultColor, out Color filColor);
                        item.color = filColor;
                    }
                }
                else
                {
                    itemsChoiceObj.SetActive(false);
                }
            }
        }
    }
}
