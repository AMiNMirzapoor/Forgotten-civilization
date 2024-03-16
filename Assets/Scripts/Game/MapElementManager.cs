using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Singleton;

public class MapElementManager : MonoBehaviour
{
    public static MapElementManager instance;

    private void Awake()
    {
        this.SetInstance(ref instance);
    }

    public void BroadcastPlayerInput(KeyCode keyCode)
    {
        // Find all GameObjects with components of type IMapElement
        Component[] components = FindObjectsOfType<Component>();
        var mapElements = components
            .OfType<IMapElement>()
            .ToArray();

        // Call OnInteract method for each map element
        foreach (var mapElement in mapElements)
        {
            mapElement.OnInteract(keyCode);
        }
    }
}
