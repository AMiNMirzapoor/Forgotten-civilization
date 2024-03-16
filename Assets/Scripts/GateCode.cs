using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateCode : UIElement
{
    [SerializeField] private TextMeshProUGUI text;
    
    public override void Show(object data)
    {
        base.Show(data);

        text.text = (string) data;
    }

    private void Update()
    {
        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        
        Hide();
    }
}
