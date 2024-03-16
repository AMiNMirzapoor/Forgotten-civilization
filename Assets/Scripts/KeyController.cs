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
            UiManager.instance.ShowKeyPressTutorial(true);
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UiManager.instance.ShowKeyPressTutorial(false);
        }
    }

    public void OnInteract(KeyCode inputKey)
    {
        Debug.Log("Interacted with " + gameObject.name + " inputkey detected => " + inputKey);
    }
}
