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
        if (pickedUpElement is not null)
        {
            if (pickedUpElement.CanBePickedUp())
            {
                PutDownItem();
            }
            
            return;
        }

        // Call OnInteract method for each map element
        foreach (var mapElement in mapElements)
        {
            bool flag = mapElement.OnInteract(keyCode);
            if (flag) 
            {
                if (mapElement.CanBePickedUp())
                {
                    PickUpItem(mapElement, parent);
                }
            }
        }
    }

    private void PickUpItem(IMapElement mapElement, Transform parent)
    {
        pickedUpElement = mapElement;
        mapElement.NotInteractable = true;
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
        pickedUpElement.GetGameObject().transform.SetParent(transform);
        pickedUpElement.GetGameObject().GetComponent<Rigidbody>().isKinematic = false;
        pickedUpElement.GetGameObject().GetComponent<Rigidbody>().useGravity = true;
        pickedUpElement.NotInteractable = false;
        pickedUpElement = null;
    }
}
