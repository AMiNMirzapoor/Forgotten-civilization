using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIElement : MonoBehaviour
{
    public virtual void Show()
    {
        gameObject.SetActive(true);
        GetComponentInChildren<CanvasGroup>().DOKill();
        GetComponentInChildren<CanvasGroup>().alpha = 0f;
        GetComponentInChildren<CanvasGroup>().DOFade(1f, 0.5f);
    }
    
    public virtual void Show(object data)
    {
        Show();
    }

    public virtual void Hide()
    {
        GetComponentInChildren<CanvasGroup>().DOKill();
        GetComponentInChildren<CanvasGroup>().DOFade(0f, 0.5f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
