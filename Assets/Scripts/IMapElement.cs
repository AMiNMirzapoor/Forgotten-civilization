using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define the interface
public interface IMapElement
{
    public bool IsPlayerNearby { get; set; }
    public bool Interactable { get; set; }
    public void OnTriggerEnter(Collider other);
    public void OnTriggerExit(Collider other);
    public void OnInteract(KeyCode inputKey);
}
