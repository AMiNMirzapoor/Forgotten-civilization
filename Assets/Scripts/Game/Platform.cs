using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour, IMapElement
{
    public GameObject GetGameObject() => gameObject;
    public bool IsPlayerNearby { get; set; }
    public bool NotInteractable { get; set; }
    public Material selectedMaterial;
    public Material deselectedMaterial;
    public MeshRenderer meshRenderer;
    private enum State {None, Selected, Deselected};
    private State state = State.None;
    public bool CanBePickedUp() => false;
    
    public Vector3 InitialRotation { get; set; }

    private List<GameObject> triggerObjects = new();

    [SerializeField] private StairBuilding stairBuilding;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || 
            (!other.isTrigger && other.gameObject.GetComponentInParent<Rigidbody>().mass > 2f))
        {
            triggerObjects.Add(other.gameObject);
            if (state != State.Selected) 
            {
                IsPlayerNearby = true;
                state = State.Selected;
                meshRenderer.material = selectedMaterial;
                stairBuilding.MoveStairUp();
            }
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (triggerObjects.Contains(other.gameObject))
        {
            triggerObjects.Remove(other.gameObject);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            IsPlayerNearby = false;
        }

        if (triggerObjects.Count == 0)
        {
            state = State.Deselected;
            meshRenderer.material = deselectedMaterial;
            stairBuilding.MoveStairDown();
        }
    }

    public bool OnInteract(KeyCode inputKey, IMapElement pickedUpElement)
    {
        return false;
    }
}
