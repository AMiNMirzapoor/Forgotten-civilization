using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StairBuilding : MonoBehaviour
{
    [SerializeField] private Transform middleStair;

    private void Start()
    {
        middleStair.localPosition -= 1f * Vector3.up;
    }


    public void MoveStairUp()
    {
        middleStair.DOKill();
        middleStair.DOLocalMoveY(0f, 2f).SetEase(Ease.Linear);
    }
    
    public void MoveStairDown()
    {
        middleStair.DOKill();
        middleStair.DOLocalMoveY(-1f, 2f).SetEase(Ease.Linear);
    }
}
