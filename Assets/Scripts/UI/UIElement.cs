using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour
{
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    
    public virtual void Show(object data)
    {
        Show();
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
