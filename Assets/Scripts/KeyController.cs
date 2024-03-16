using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour, IMapElement
{
    public bool IsPlayerNearby { get; set; }
    public bool Interactable { get; set; }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsPlayerNearby = true;
            UiManager.instance.ShowKeyPressTutorial();
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsPlayerNearby = false;
            UiManager.instance.HideKeyPressTutorial();
        }
    }

    public void OnInteract(KeyCode inputKey)
    {
        if (!IsPlayerNearby)
        {
            return;
        }
        Debug.Log("Interacted with " + gameObject.name + " inputkey detected => " + inputKey);
        
        InventoryManager.instance.hasKey = true;
        UiManager.instance.ShowKeyInventory();
        gameObject.SetActive(false);
    }
}
