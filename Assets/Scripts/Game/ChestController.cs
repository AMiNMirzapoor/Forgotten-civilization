using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChestController : MonoBehaviour, IMapElement
{
    public GameObject GetGameObject() => gameObject;
    public bool IsPlayerNearby { get; set; }
    public bool NotInteractable { get; set; }
    private bool inAnimationPlayed = false;
    private float animationLength = 5;
    [SerializeField] private Animator anim;
    public bool CanBePickedUp() => false;
    public Vector3 InitialRotation { get; set; }

    private bool isOpened;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsPlayerNearby = true;
            if (isOpened)
            {
                UiManager.instance.ShowGateCode();
                return;
            }
            if (MapElementManager.instance.pickedUpElement is null or not KeyController)
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
            if (isOpened)
            {
                UiManager.instance.HideGateCode();
                return;
            }
            if (MapElementManager.instance.pickedUpElement is null or not KeyController)
            {
                UiManager.instance.HideKeyPressTutorial();
            }
            else
            {
                UiManager.instance.HideKeyNeededTutorial();
            }
        }
    }

    public bool OnInteract(KeyCode inputKey, IMapElement pickedUpElement)
    {
        if (!IsPlayerNearby)
        {
            return false;
        }

        if (NotInteractable)
        {
            return false;
        }
        
        if (pickedUpElement is KeyController key)
        {
            ShowOpenAnimation();
            key.NotInteractable = true;
            key.transform.SetParent(transform);
            key.transform.DOKill();
            key.transform.DOMove(transform.position, 1f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                if (IsPlayerNearby)
                {
                    UiManager.instance.ShowGateCode();
                }
                NotInteractable = false;
                isOpened = true;
            });
            UiManager.instance.HideKeyInventory();
            UiManager.instance.HideKeyPressTutorial();
            
            NotInteractable = true;
            return true;
        }

        return false;
    }

    public void ShowOpenAnimation()
    {
        if (!inAnimationPlayed)
        {
            if(anim != null)
            {
                anim.enabled = true;
                //anim.Play("Chest", 0 ,0.01f);
            }
            inAnimationPlayed = true;
        }
    }
}
