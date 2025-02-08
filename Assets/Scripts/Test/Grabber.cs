using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Grabber : MonoBehaviour
{
    private GameObject selectObject;
    public GameObject correctForm;
    private bool isMoving;

    private float startPosX;
    private float startPosY;

    // Update is called once per frame
    void Update()
    {
        firstDragVersion();


    }

    private void secondDragVersion()
    {
        if (isMoving)
        {
            Vector3 position = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

            this.gameObject.transform.localPosition = new Vector3(worldPosition.x-startPosX, worldPosition.y - startPosY, this.gameObject.transform.localPosition.z);

            isMoving = true;
        }
    }

    private void firstDragVersion()
    {

      
            if (selectObject != null)
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                selectObject.transform.position = new Vector3(worldPosition.x, .25f, worldPosition.z);
            }
        
    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar-worldMousePosNear, out hit);

        return hit;
    }

    private void OnMouseUp()
    {
       // isMoving = false;
        Debug.Log("up");

        if (selectObject != null)
        {
            Debug.Log("up1");
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectObject.transform.position = new Vector3(worldPosition.x, .132f, worldPosition.z);

            Debug.Log(selectObject.transform.localPosition.x - correctForm.transform.localPosition.x);
            Debug.Log(selectObject.transform.localPosition.z - correctForm.transform.localPosition.z);

            if (Mathf.Abs(selectObject.transform.localPosition.x - correctForm.transform.localPosition.x)<=0.5f &&
                Mathf.Abs(selectObject.transform.localPosition.z - correctForm.transform.localPosition.z) <= 0.5f)
            {
                Debug.Log("close");
                selectObject.transform.localPosition = new Vector3(correctForm.transform.localPosition.x, .132f, correctForm.transform.localPosition.z);
            }

            selectObject = null;
            Cursor.visible = true;

        }



    }
    private void OnMouseDown()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("down");
        //   Vector3 position = Input.mousePosition;
        //    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

        //    startPosX = worldPosition.x - this.transform.localPosition.x;
        //    startPosY = worldPosition.y - this.transform.localPosition.y;

        //    isMoving = true;

        // }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("down");
            if (selectObject == null)
            {
                RaycastHit hit = CastRay();
                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("Drag"))
                    {
                        return;
                    }
                    selectObject = hit.collider.gameObject;
                    Cursor.visible = false;
                }
            }
            //else
           // {
           //     Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectObject.transform.position).z);
           //     Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            //    selectObject.transform.position = new Vector3(worldPosition.x, .132f, worldPosition.z);

             //   selectObject = null;
            //    Cursor.visible = true;
           // }
        }

    }
}
