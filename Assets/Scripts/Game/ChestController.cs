using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour, IMapElement
{
    public GameObject GetGameObject() => gameObject;
    public bool IsPlayerNearby { get; set; }
    public bool NotInteractable { get; set; }

    public bool CanBePickedUp() => false;
    public Vector3 InitialRotation { get; set; }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsPlayerNearby = true;
            if (InventoryManager.instance.hasKey)
            {
                UiManager.instance.ShowKeyPressTutorial();
            }
            else
            {
                UiManager.instance.ShowKeyNeededTutorial();
            }
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsPlayerNearby = false;
            if (InventoryManager.instance.hasKey)
            {
                UiManager.instance.HideKeyPressTutorial();
            }
            else
            {
                UiManager.instance.HideKeyNeededTutorial();
            }
        }
    }

    public bool OnInteract(KeyCode inputKey)
    {
        if (!IsPlayerNearby)
        {
            return false;
        }
        
        if (InventoryManager.instance.hasKey)
        {
            // InventoryManager.instance.hasKey = false;
            UiManager.instance.HideKeyInventory();
            UiManager.instance.HideKeyPressTutorial();
            UiManager.instance.ShowGateCode();

            return true;
        }

        return false;
    }
}
