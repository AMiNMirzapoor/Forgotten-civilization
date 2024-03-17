using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GateController : MonoBehaviour, IMapElement
{
    public GameObject GetGameObject() => gameObject;
    [SerializeField] private float targetY = 10f; // Target Y position
    private bool inAnimationPlayed = false;
    public bool IsPlayerNearby { get; set; }
    public bool NotInteractable { get; set; }
    public bool CanBePickedUp() => false;
    [SerializeField] private GameObject trigger;
    public Vector3 InitialRotation { get; set; }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UiManager.instance.ShowWinMenu();
            Time.timeScale = 0.00001f;
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
    }

    public bool OnInteract(KeyCode inputKey, IMapElement pickedUpElement)
    {
        return false;
    }

    public void ShowOpenAnimation()
    {
        if (!inAnimationPlayed)
        {
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = new Vector3(currentPosition.x, targetY, currentPosition.z);
            transform.DOMove(targetPosition, 2f); // Move to the target position in 2 seconds
            inAnimationPlayed = true;
            trigger.SetActive(true);
        }
    }
}

