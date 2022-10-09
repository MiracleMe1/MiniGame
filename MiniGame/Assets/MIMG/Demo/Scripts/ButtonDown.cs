using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonDown : MonoBehaviour, IPointerDownHandler
{
    [Header("Events")]
    [Space]

    public UnityEvent OnDownEvent;


    void Awake()
    {
        if (OnDownEvent == null)
            OnDownEvent = new UnityEvent();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDownEvent.Invoke();
    }
}
