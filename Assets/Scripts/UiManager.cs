using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Singleton;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    private Canvas canvas;

    [SerializeField] private UIElement keyPressTutorial;
    [SerializeField] private UIElement keyInInventory;
    [SerializeField] private UIElement gateCode;

    private void Awake()
    {
        this.SetInstance(ref instance);
    }

    private void Start()
    {
        canvas = transform.Find("Canvas").GetComponent<Canvas>();

        UIElement[] uiElements = canvas.GetComponentsInChildren<UIElement>();
        for (int i = 0; i < uiElements.Length; i++)
        {
            uiElements[i].Hide();
        }
    }

    public void ShowKeyPressTutorial()
    {
        keyPressTutorial.Show();
    }
    
    public void HideKeyPressTutorial()
    {
        keyPressTutorial.Hide();
    }
    
    public void ShowKeyInventory()
    {
        keyInInventory.Show();
    }
    
    public void HideKeyInventory()
    {
        keyInInventory.Hide();
    }

    public void ShowGateCode(string code)
    {
        gateCode.Show(code);
    }
    
    public void HideGateCode()
    {
        gateCode.Hide();
    }
}