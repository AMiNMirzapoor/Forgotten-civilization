using System;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Singleton;
using Utils;

public class MapElementManager : MonoBehaviour
{
    public static MapElementManager instance;

    private IMapElement[] mapElements;
    public IMapElement pickedUpElement;

    private void Awake()
    {
        this.SetInstance(ref instance);
    }

    private void Start()
    {
        // Find all GameObjects with components of type IMapElement
        Component[] components = FindObjectsOfType<Component>();
        mapElements = components.OfType<IMapElement>().ToArray();
    }

    public void BroadcastPlayerInput(Transform parent, KeyCode keyCode)
    {
        bool flag = false;
        // Call OnInteract method for each map element
        foreach (IMapElement mapElement in mapElements)
        {
            bool interacted = mapElement.OnInteract(keyCode, pickedUpElement);
            if (interacted)
            {
                flag = true;
                if (pickedUpElement is not null)
                {
                    pickedUpElement = null;
                }
                if (mapElement.CanBePickedUp())
                {
                    PickUpItem(mapElement, parent);
                    Debug.Log(mapElement.GetGameObject().name + " Collected");
                }
                else
                {
                    Debug.Log(mapElement.GetGameObject().name + " Interacted");
                }
            }
        }
        
        if (!flag && 
            pickedUpElement is not null)
        {
            if (pickedUpElement.CanBePickedUp())
            {
                Debug.Log(pickedUpElement.GetGameObject().name + " PutDown");
                PutDownItem();
            }
        }
    }

    private void PickUpItem(IMapElement mapElement, Transform parent)
    {
        pickedUpElement = mapElement;
        mapElement.NotInteractable = true;
        mapElement.IsPlayerNearby = false;
        mapElement.GetGameObject().transform.SetParent(parent);
        mapElement.GetGameObject().GetComponent<Rigidbody>().isKinematic = true;
        mapElement.GetGameObject().GetComponent<Rigidbody>().useGravity = false;
        mapElement.GetGameObject().transform.eulerAngles = mapElement.InitialRotation;
        Bounds bounds = DMath.GetBounds(mapElement.GetGameObject(), false);
        Vector3 position = new Vector3(0, 2 + bounds.size.y / 2, 0);
        mapElement.GetGameObject().transform.DOLocalMove(position, 0.8f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            mapElement.GetGameObject().transform.DOLocalMove(position + 0.1f * Vector3.up, 0.75f)
                .SetEase(Ease.InOutQuad)
                .SetLoops(-1, LoopType.Yoyo);
        });
    }

    private void PutDownItem()
    {
        pickedUpElement.GetGameObject().transform.DOKill();
        Transform parent = pickedUpElement.GetGameObject().transform.parent;
        pickedUpElement.GetGameObject().transform.SetParent(transform);
        pickedUpElement.GetGameObject().GetComponent<Rigidbody>().isKinematic = false;
        pickedUpElement.GetGameObject().GetComponent<Rigidbody>().useGravity = true;
        pickedUpElement.GetGameObject().GetComponent<Rigidbody>().AddForce(3.5f*parent.forward, ForceMode.VelocityChange);
        pickedUpElement.NotInteractable = false;
        pickedUpElement.IsPlayerNearby = false;
        pickedUpElement = null;
    }
}
