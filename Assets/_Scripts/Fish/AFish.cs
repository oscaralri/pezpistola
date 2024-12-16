using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AFish : MonoBehaviour, IPointerClickHandler
{
    FishType fishType {get;}
    [SerializeField] private Collider hitCollider;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.transform.GetComponent<Collider>());
        if(eventData.pointerCurrentRaycast.gameObject.transform.GetComponent<Collider>() == hitCollider.gameObject)
        {
            Debug.Log("golpe");
        }
    }
}

public enum FishType {Red, Yellow, Blue}
