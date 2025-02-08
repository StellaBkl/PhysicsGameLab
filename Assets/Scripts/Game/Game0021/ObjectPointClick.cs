using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPointClick : MonoBehaviour
{
    // Start is called before the first frame update
    void OnMouseDown()
    {
        Debug.Log("Click point");
        SelectPoint selectPoint = FindObjectOfType<SelectPoint>();
        selectPoint.OnPointClick(gameObject.name);
    }
}