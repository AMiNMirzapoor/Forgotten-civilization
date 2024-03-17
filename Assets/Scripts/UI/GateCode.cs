using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GateCode : UIElement
{
    [SerializeField] private TextMeshProUGUI text;
    
    public override void Show()
    {
        base.Show();
        GetComponentInChildren<CanvasGroup>().DOKill();
        GetComponentInChildren<CanvasGroup>().alpha = 0f;
        GetComponentInChildren<CanvasGroup>().DOFade(1f, 0.5f);

        StartCoroutine(HideAfterDelay());
    }
    
    public override void Hide()
    {
        GetComponentInChildren<CanvasGroup>().DOFade(0f, 0.5f).OnComplete(() =>
        {
            base.Hide();
        });
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        
        Hide();
    }
}
