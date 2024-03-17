using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour, IMapElement
{
    public GameObject GetGameObject() => gameObject;
    public bool IsPlayerNearby { get; set; }
    public bool NotInteractable { get; set; }
    public bool CanBePickedUp() => true;
    
    public Vector3 InitialRotation { get; set; }

    private void Start()
    {
        InitialRotation = transform.eulerAngles;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (NotInteractable)
        {
            return;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            IsPlayerNearby = true;
            UiManager.instance.ShowKeyPressTutorial();
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (NotInteractable)
        {
            return;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            IsPlayerNearby = false;
            UiManager.instance.HideKeyPressTutorial();
        }
    }

    public bool OnInteract(KeyCode inputKey, IMapElement pickedUpElement)
    {
        if (NotInteractable)
        {
            return false;
        }
        
        if (!IsPlayerNearby)
        {
            return false;
        }

        if (pickedUpElement is not null)
        {
            return false;
        }
        
        UiManager.instance.ShowKeyInventory();
        UiManager.instance.HideKeyPressTutorial();
        return true;
    }
}
