using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define the interface
public interface IMapElement
{
    public GameObject GetGameObject();
    public bool IsPlayerNearby { get; set; }
    public bool NotInteractable { get; set; }
    public void OnTriggerEnter(Collider other);
    public void OnTriggerExit(Collider other);
    public bool OnInteract(KeyCode inputKey, IMapElement pickedUpElement);

    public bool CanBePickedUp();
    
    public Vector3 InitialRotation { get; set; }
}
