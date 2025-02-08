using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private string objectType;
    private bool grabbed;

    private void Awake()
    {
        Debug.Log("Awake");
    }

    private void Update()
    {
        if (grabbed)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(gameObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            gameObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
        }
    }

    private void OnMouseDown()
    {
        Debug.Log($"Grabbed {objectType}");
        grabbed = true;
    }

    private void OnMouseUp()
    {
        Debug.Log($"Released {objectType}");
        grabbed = false;

        //Todo: raycast to detect spawn points
        RaycastHit hit = CastRay();
        Debug.Log(hit.transform.name);
        if (hit.transform != null && hit.transform.CompareTag("Drag")) {
            Debug.Log("Win win");
        }
        // if spawn point found
        // get component or get tag and if matches the place the gameobject there
        // place gameobject.transform at hit object transform
    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;
    }
}
