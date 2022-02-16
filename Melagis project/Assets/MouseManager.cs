using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    InputControls controls;
    public GameObject selectedCard;
    float zIncrement = 0.001f;
    private void Awake()
    {
        controls = new InputControls();
        controls.Enable();
        controls.MarozasControls.MouseClick.performed += AssignCard;
    }
    private GameObject GetSelectedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(controls.MarozasControls.MousePosition.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.CompareTag("Card"))
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }
    private void AssignCard(InputAction.CallbackContext obj)
    {
        if (selectedCard != null)
        {
            selectedCard.transform.position = new Vector3(
                selectedCard.transform.position.x,
                selectedCard.transform.position.y,
                selectedCard.transform.position.z - zIncrement);

            selectedCard.GetComponent<BoxCollider>().enabled = true;
            selectedCard = null;
            return;
        }
        selectedCard = GetSelectedObject();
    }


    void Update()
    {
        if(selectedCard!=null)
        { 
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(controls.MarozasControls.MousePosition.ReadValue<Vector2>());
            mousePos.z = 0.0f;
            selectedCard.transform.position = mousePos;
            selectedCard.GetComponent<BoxCollider>().enabled = false;
        }
        
    }
}
