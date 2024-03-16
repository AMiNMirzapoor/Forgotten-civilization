using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SymbolController : MonoBehaviour, IMapElement
{
    public bool IsPlayerNearby { get; set; }
    public bool Interactable { get; set; }
    public Material selectedMaterial;
    public Material deselectedMaterial;
    public MeshRenderer meshRenderer;
    private enum State {None, Selected, Deselected};
    private State state = State.None;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (state != State.Selected) 
            {
                IsPlayerNearby = true;
                state = State.Selected;
                meshRenderer.material = selectedMaterial; 
                PuzzleManager.instance.SymbolSelected(gameObject.name);
            }
        }
    }

    public void UpdateState(bool isSelected)
    {
        if (!isSelected)
        {
            state = State.Deselected;
            meshRenderer.material = deselectedMaterial;
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsPlayerNearby = false;
        }
    }

    public void OnInteract(KeyCode inputKey)
    {
    }
}

