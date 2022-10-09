using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GAME_MENU : MonoBehaviour
{
    public GameObject PanelMain;
    public Button mainButtonPlay;
    public Button mainButtonSettings;
    [Space(10)]
    public GameObject PanelGame;
    public Button gameButtonSettings;
    [Space(10)]
    public GameObject PanelSettings;
    public Button settingsButtonSettingButtons;
    public Button settingsButtonClose;
    public Button settingsButtonMainMenu;


    void Start()
    {
        mainButtonPlay.onClick.RemoveAllListeners();
        mainButtonPlay.onClick.AddListener(() =>
        {
            ClickPlay();
        });

        mainButtonSettings.onClick.RemoveAllListeners();
        mainButtonSettings.onClick.AddListener(() =>
        {
            ClickSettings();
        });

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        gameButtonSettings.onClick.RemoveAllListeners();
        gameButtonSettings.onClick.AddListener(() =>
        {
            ClickSettings();
        });

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        settingsButtonSettingButtons.onClick.RemoveAllListeners();
        settingsButtonSettingButtons.onClick.AddListener(() =>
        {
            if (MIMG_settings._MIMG_settings != null)
                MIMG_settings._MIMG_settings.ShowMIMG();    // We cause the window to display with the settings of the buttons
        });

        settingsButtonClose.onClick.RemoveAllListeners();
        settingsButtonClose.onClick.AddListener(() =>
        {
            ClickSettingsClose();
        });

        settingsButtonMainMenu.onClick.RemoveAllListeners();
        settingsButtonMainMenu.onClick.AddListener(() =>
        {
            ClickMainMenu();
        });

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ClickMainMenu();
    }

    void ClickPlay()
    {
        PanelMain.SetActive(false);
        PanelGame.SetActive(true);
    }

    void ClickSettings()
    {
        PanelSettings.SetActive(true);

        settingsButtonMainMenu.gameObject.SetActive(false);
        if (PanelGame.activeSelf)
            settingsButtonMainMenu.gameObject.SetActive(true);
    }

    void ClickSettingsClose()
    {
        PanelSettings.SetActive(false);
    }

    void ClickMainMenu()
    {
        PanelMain.SetActive(true);
        PanelGame.SetActive(false);
        PanelSettings.SetActive(false);
    }
}
