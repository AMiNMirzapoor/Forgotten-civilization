using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public virtual void Show(object data)
    {
        Show();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
