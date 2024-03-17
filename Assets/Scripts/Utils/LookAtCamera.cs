using DG.Tweening;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 initialScale;
    private Vector3 initialOffset;

    public bool updatePosition;
    
    private void Start()
    {
        initialScale = transform.localScale;
        transform.localScale = Vector3.zero;
        cameraTransform = Camera.main.transform;
        initialOffset = transform.localPosition;
    }

    public void Show()
    {
        transform.DOKill();
        transform.DOScale(initialScale, 0.2f);
    }
    
    public void Hide()
    {
        transform.DOKill();
        transform.DOScale(0f, 0.2f);
    }

    private void Update()
    {
        transform.LookAt(cameraTransform);
        if (updatePosition)
        {
            transform.position = transform.parent.position + initialOffset;
        }
    }
}