using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MIMG_button_game : MonoBehaviour
{
    class imgTransparency
    {
        public Image img;
        public Text text;
        public float a;
    }

    public ButtonsGame buttonsGame;

    RectTransform m_RectTransform = null;
    List<imgTransparency> imgs = new List<imgTransparency>();


    private void Awake()
    {
        if (m_RectTransform == null)
            m_RectTransform = GetComponent<RectTransform>();

        foreach (Image img in GetComponentsInChildren<Image>())
        {
            imgs.Add(new imgTransparency()
            {
                img = img,
                text = null,
                a = img.color.a
            });
        }
        foreach (Text text in GetComponentsInChildren<Text>())
        {
            imgs.Add(new imgTransparency()
            {
                img = null,
                text = text,
                a = text.color.a
            });
        }
    }

    public void SetSize(float Size)
    {
        if (m_RectTransform != null)
        {
            m_RectTransform.localScale = Vector3.one * Size;
        }
    }

    public void SetPosition(Vector2 Position)
    {
        if (m_RectTransform != null)
        {
            m_RectTransform.anchoredPosition3D = new Vector3(Position.x, Position.y, 0);
        }
    }

    public void SetTransparency(float Transparency)
    {
        Color c = Color.black;
        foreach (imgTransparency g in imgs)
        {
            if (g.img != null)
                c = g.img.color;
            else
                c = g.text.color;
            c.a = g.a * Transparency;
            if (g.img != null)
                g.img.color = c;
            else
                g.text.color = c;
        }
    }
}
