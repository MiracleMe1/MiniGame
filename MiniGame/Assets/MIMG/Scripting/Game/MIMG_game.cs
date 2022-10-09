using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MIMG_game : MonoBehaviour
{
    public static MIMG_game _MIMG_game;

    private List<MIMG_button_game> _MIMG_buttons_game = new List<MIMG_button_game>();


    private void Awake()
    {
        _MIMG_game = this;

        _MIMG_buttons_game = gameObject.GetComponentsInChildren<MIMG_button_game>().ToList();
    }

    void Start()
    {
        UpdateNowScheme();
    }

    // Updating the location of buttons in the game
    public void UpdateNowScheme()
    {
        List<MIMG_settings.SaveButton> _scheme = MIMG_settings.GetNowScheme();

        foreach (MIMG_button_game bu in _MIMG_buttons_game)
        {
            foreach (MIMG_settings.SaveButton sb in _scheme)
            {
                if (bu.buttonsGame == sb.buttonsGame)
                {
                    bu.SetSize(sb.m_Size);
                    bu.SetPosition(sb.m_Position);
                    bu.SetTransparency(sb.m_Transparency);

                    break;
                }
            }
        }
    }
}
