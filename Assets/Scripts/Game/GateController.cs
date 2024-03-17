using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GateController : MonoBehaviour, IMapElement
{
    public GameObject GetGameObject() => gameObject;
    [SerializeField] private float targetY = 10f; // Target Y position
    private bool inAnimationPlayed = false;
    private float animationLength = 5;
    [SerializeField] private Animator anim;
    public bool IsPlayerNearby { get; set; }
    public bool NotInteractable { get; set; }
    public bool CanBePickedUp() => false;
    [SerializeField] private GameObject trigger;
    public Vector3 InitialRotation { get; set; }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UiManager.instance.ShowWinMenu();
            Time.timeScale = 0.00001f;
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
    }

    public bool OnInteract(KeyCode inputKey, IMapElement pickedUpElement)
    {
        return false;
    }

    void Start(){
        ShowOpenAnimation();
    }

    public void ShowOpenAnimation()
    {
        if (!inAnimationPlayed)
        {
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = new Vector3(currentPosition.x, targetY, currentPosition.z);
            transform.DOMove(targetPosition, animationLength); // Move to the target position in 2 seconds
            if(anim != null)
            {
                //anim.Play("gate2", 0 ,0.01f);
                anim.enabled = true;
                StartCoroutine(ExampleCoroutine());
            }
            inAnimationPlayed = true;
            trigger.SetActive(true);
        }
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(animationLength); // Wait for 3 seconds
        stopAfterPlay();
    }

    private void stopAfterPlay(){
        anim.enabled = false;
    }
}

