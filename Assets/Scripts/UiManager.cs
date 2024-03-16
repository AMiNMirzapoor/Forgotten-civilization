using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Singleton;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    private Canvas canvas;

    [SerializeField] private GameObject keyPressTutorial;
    [SerializeField] private GameObject keyInInentory;

    private void Awake()
    {
        this.SetInstance(ref instance);
    }

    private void Start()
    {
        canvas = transform.Find("Canvas").GetComponent<Canvas>();
    }

    public void ShowKeyPressTutorial(bool show)
    {
        keyPressTutorial.SetActive(show);
    }
}