using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[ExecuteInEditMode]
public class MIMG_button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ButtonsGame buttonsGame;

    public float Size
    {
        get { return m_Size; }
        set
        {
            m_Size = value;
            UpdateButton();
        }
    }
    public float Transparency
    {
        get { return m_Transparency; }
        set
        {
            m_Transparency = value;
            UpdateButton();
        }
    }
    public Vector2 Position
    {
        get { return m_Position; }
        set
        {
            m_Position = value;
            UpdateButton();
        }
    }

    [SerializeField, Range(0.1f, 2.5f)]
    private float m_Size = 1;
    [SerializeField, Range(0f, 1f)]
    private float m_Transparency = 1;
    [SerializeField]
    private Vector2 m_Position = Vector2.zero;

    public Sprite SelectSprite;
    public Sprite UnselectSprite;


    RectTransform m_RectTransform = null;
    Canvas m_Canvas = null;
    RectTransform m_Canvas_RectTransform = null;
    Button buttonSel;
    GameObject SelEdges;
    Image SelImage;
    Image UnselImage;

    bool IsSelect = false;
    bool IsTouched = false;
    Vector3 oldMousePosition = Vector3.zero;



    void OnValidate()
    {
        if(m_RectTransform != null)
            m_RectTransform.anchoredPosition3D = m_Position;

        UpdateButton();
    }

    private void Awake()
    {
        if (m_Canvas == null)
            m_Canvas = GetComponentInParent<Canvas>();

        if (m_Canvas_RectTransform == null && m_Canvas != null)
            m_Canvas_RectTransform = m_Canvas.GetComponent<RectTransform>();

        if (m_RectTransform == null)
            m_RectTransform = GetComponent<RectTransform>();

        if (buttonSel == null)
            buttonSel = GetComponent<Button>();

        if (SelEdges == null)
            SelEdges = transform.Find("SelEdges").gameObject;

        if (SelImage == null)
            SelImage = transform.Find("SelImage").GetComponent<Image>();

        if (UnselImage == null)
            UnselImage = transform.Find("UnselImage").GetComponent<Image>();
    }

    void Start()
    {
        if (!(Application.isEditor && !Application.isPlaying))
        {
            buttonSel.onClick.RemoveAllListeners();
            buttonSel.onClick.AddListener(() =>
            {
                Select();
            });

            SelImage.sprite = SelectSprite;
            UnselImage.sprite = UnselectSprite;

            UnSelect();
        }
    }


    void Update()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            if (m_RectTransform != null)
            {
                m_Position = m_RectTransform.anchoredPosition3D;
                m_Size = m_RectTransform.localScale.z;

                UpdateButton();
            }
        }
        else
        {
            if (IsSelect && IsTouched)
            {
                if (Input.touchCount != 1 && !Input.GetMouseButton(0))
                    IsTouched = false;
                else
                {
                    Position += WorldToCanvas((Vector2)Camera.main.ScreenToWorldPoint(Input.GetMouseButton(0) ? (Input.mousePosition - oldMousePosition) : (Vector3)Input.touches[0].deltaPosition));
                    oldMousePosition = Input.mousePosition;
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Select();
        oldMousePosition = Input.mousePosition;
        IsTouched = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsTouched = false;
    }

    Vector2 WorldToCanvas(Vector3 world_position)
    {
        var viewport_position = Camera.main.WorldToViewportPoint(world_position);

        return new Vector2((viewport_position.x * m_Canvas_RectTransform.sizeDelta.x),
                           (viewport_position.y * m_Canvas_RectTransform.sizeDelta.y));
    }

    void UpdateButton()
    {
        if (m_RectTransform != null)
        {
            m_RectTransform.localScale = Vector3.one * Size;

            if (m_Canvas_RectTransform != null)
            { 
                Vector2 size_button = Vector2.Scale(m_RectTransform.sizeDelta / 2, m_RectTransform.localScale);
                m_Position = new Vector2(
                    Mathf.Clamp(Position.x, m_Canvas_RectTransform.offsetMin.x + size_button.x, m_Canvas_RectTransform.offsetMax.x - size_button.x),
                    Mathf.Clamp(Position.y, m_Canvas_RectTransform.offsetMin.y + size_button.y, m_Canvas_RectTransform.offsetMax.y - size_button.y));
            }
            m_RectTransform.anchoredPosition3D = new Vector3(Position.x, Position.y, 0);
        }

        if (SelImage != null && UnselImage != null)
        {
            Color c = Color.white;
            c.a = Transparency;
            SelImage.color = c;
            UnselImage.color = c;
        }
    }

    // select the button
    void Select()
    {
        MIMG_settings._MIMG_settings.SelectButton(this);
        IsSelect = true;

        SelEdges.SetActive(true);
        SelImage.gameObject.SetActive(true);
        UnselImage.gameObject.SetActive(false);
    }

    // deactivate button selection
    public void UnSelect()
    {
        IsSelect = false;
        IsTouched = false;

        SelEdges.SetActive(false);
        SelImage.gameObject.SetActive(false);
        UnselImage.gameObject.SetActive(true);
    }
}
