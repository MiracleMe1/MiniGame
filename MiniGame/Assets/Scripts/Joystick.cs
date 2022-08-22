using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : ScrollRect
{
    protected float mRadius = 0f;
    
    public Vector2 dragDirection = new Vector2(0, 0);
    public Vector2 dragStrength = new Vector2(0, 0);
    public float isDrag = 0f;
    public GameObject Bow;
    protected override void Start()
    {
        base.Start();
        mRadius = (transform as RectTransform).sizeDelta.x * 0.45f;
        Bow=GameObject.Find("Bow");
    }
    
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        isDrag = 1f;
        var contentPosition = this.content.anchoredPosition;
        if (contentPosition.magnitude > mRadius)
        {
            contentPosition = contentPosition.normalized * mRadius;
            SetContentAnchoredPosition(contentPosition);
        }

        dragDirection = contentPosition.normalized;
        dragStrength = contentPosition;

    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        isDrag = 0f;
        dragDirection = new Vector2(0, 0);
        Bow.GetComponent<Bow>().Shoot();
    }

}
