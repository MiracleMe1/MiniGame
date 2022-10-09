using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum AxisRawStick
{
    Horizontal,
    Vertical
}

public class ButtonStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform ObjEdge;
    public RectTransform TrStick;

    bool IsTouched = false;
    int pointerID = -1;
    Vector2 oldPosition = Vector2.zero;
    Canvas m_Canvas = null;
    RectTransform m_Canvas_RectTransform = null;

    float Radius = 50;
    float stickDeltaMovePosition = 20;

    Vector2 deltaMove = Vector2.zero;


    private void Awake()
    {
        if (m_Canvas == null)
            m_Canvas = GetComponentInParent<Canvas>();

        if (m_Canvas_RectTransform == null && m_Canvas != null)
            m_Canvas_RectTransform = m_Canvas.GetComponent<RectTransform>();
    }

    void Start()
    {
        Unclick();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!IsTouched)
        {
            pointerID = eventData.pointerId;
            oldPosition = eventData.position;
            Click();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerID)
        {
            Unclick();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsTouched && eventData.pointerId == pointerID)
        {
            deltaMove = (eventData.position - oldPosition) / Radius;
            deltaMove.x = Mathf.Clamp(deltaMove.x, -1, 1);
            deltaMove.y = Mathf.Clamp(deltaMove.y, -1, 1);

            TrStick.anchoredPosition = deltaMove * stickDeltaMovePosition;
        }
    }

    public float GetAxisRaw(AxisRawStick axis)
    {
        return axis == AxisRawStick.Horizontal ? deltaMove.x : deltaMove.y;
    }

    void Unclick()
    {
        IsTouched = false;
        deltaMove = Vector2.zero;
        pointerID = -1;
        ObjEdge.gameObject.SetActive(false);
    }

    void Click()
    {
        ObjEdge.gameObject.SetActive(true);

        ObjEdge.anchoredPosition = WorldToCanvas(Camera.main.ScreenToWorldPoint(oldPosition - new Vector2(Screen.width, Screen.height) / 2));

        IsTouched = true;
    }

    Vector2 WorldToCanvas(Vector3 world_position)
    {
        var viewport_position = Camera.main.WorldToViewportPoint(world_position);

        return new Vector2((viewport_position.x * m_Canvas_RectTransform.sizeDelta.x),
                           (viewport_position.y * m_Canvas_RectTransform.sizeDelta.y));
    }
}
