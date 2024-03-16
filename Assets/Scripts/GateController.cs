using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GateController : MonoBehaviour, IMapElement
{
    [SerializeField] private float targetY = 10f; // Target Y position
    private bool inAnimationPlayed = false;
    public bool IsPlayerNearby { get; set; }
    public bool Interactable { get; set; }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UiManager.instance.ShowKeyPressTutorial();
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UiManager.instance.HideKeyPressTutorial();
        }
    }

    public void OnInteract(KeyCode inputKey)
    {
        Debug.Log("Interacted with " + gameObject.name + " inputkey detected => " + inputKey);
        if (!inAnimationPlayed)
        {
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = new Vector3(currentPosition.x, targetY, currentPosition.z);
            transform.DOMove(targetPosition, 2f); // Move to the target position in 2 seconds
            inAnimationPlayed = true;
        }
    }
}

