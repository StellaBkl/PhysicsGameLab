using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSelection : MonoBehaviour
{
    public GameObject currentSelection;
    private SelectShadowPosition selectShadowPosition;

    void OnMouseDown()
    {
        selectShadowPosition = FindObjectOfType<SelectShadowPosition>();
        selectShadowPosition.SelectPosition(currentSelection);
    }
}
