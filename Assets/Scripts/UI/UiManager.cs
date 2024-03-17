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
    [SerializeField] private UIElement keyNeededTutorial;
    [SerializeField] private UIElement keyInInventory;
    [SerializeField] private UIElement gateCode;
    [SerializeField] private UIElement winMenu;
    [SerializeField] private UIElement tutorialMenu;

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
        
        tutorialMenu.Show();
    }

    public void ShowKeyPressTutorial()
    {
        //keyPressTutorial.Show();
    }
    
    public void HideKeyPressTutorial()
    {
        //keyPressTutorial.Hide();
    }
    
    public void ShowKeyNeededTutorial()
    {
        //keyNeededTutorial.Show();
    }
    
    public void HideKeyNeededTutorial()
    {
        //keyNeededTutorial.Hide();
    }
    
    public void ShowKeyInventory()
    {
        //keyInInventory.Show();
    }
    
    public void HideKeyInventory()
    {
        //keyInInventory.Hide();
    }

    public void ShowGateCode()
    {
        gateCode.Show();
    }
    
    public void HideGateCode()
    {
        gateCode.Hide();
    }

    public void ShowWinMenu()
    {
        winMenu.Show();
    }

    private bool tutorialIsShowing;
    public void ShowTutorial()
    {
        if (tutorialIsShowing)
        {
            tutorialMenu.Hide();
            tutorialIsShowing = false;
        }
        else
        {
            tutorialMenu.Show();
            tutorialIsShowing = true;
        }
    }

    public void OnAnyKey()
    {
        tutorialMenu.Hide();
        gateCode.Hide();
    }
}