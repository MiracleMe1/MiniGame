using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Linq;

// enumeration of existing buttons (this is necessary to link the buttons in the game and the editor)
public enum ButtonsGame
{
    Stick,
    Attack,
    Settings,
    Jump
}

public class MIMG_settings : MonoBehaviour
{
    public static MIMG_settings _MIMG_settings;

    public GameObject PanelButtons;
    public GameObject PanelEditScheme;
    public Text TextNameScheme;
    [Space(10)]
    public Slider SliderSize;
    public Text TextPrcSize;
    public Slider SliderTransparency;
    public Text TextPrcTransparency;
    [Space(10)]
    public GameObject PanelMiddle;
    public Image IamgeBottom;
    public Sprite SpriteBottomOpen;
    public Sprite SpriteBottomClose;
    [Space(10)]
    public GameObject PanelSchemes;
    public Text TextSchemesUse;
    public InputField InputFieldSchemes1;
    public GameObject GOSchemes1Use;
    public GameObject GOSchemes1Used;
    public InputField InputFieldSchemes2;
    public GameObject GOSchemes2Use;
    public GameObject GOSchemes2Used;
    public InputField InputFieldSchemes3;
    public GameObject GOSchemes3Use;
    public GameObject GOSchemes3Used;
    [Space(10)]
    public GameObject PanelQReset;
    [Space(10)]
    public GameObject PanelQSave;
    [Space(10)]
    


    private bool IsEditButtons = false; // whether there were any unsaved changes
    private MIMG_button mimg_button = null; // Button that can be customized
    private float k_move = 10; // button movement coefficient
    private List<MIMG_button> _MIMG_button = new List<MIMG_button>();

    // button class to save and load
    public class SaveButton
    {
        public ButtonsGame buttonsGame;
        public float m_Size;
        public float m_Transparency;
        public Vector2 m_Position;
    }


    private void Awake()
    {
        if (_MIMG_settings != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        _MIMG_settings = this;

        _MIMG_button = PanelButtons.GetComponentsInChildren<MIMG_button>().ToList();
    }

    void Start()
    {
        HideMIMG();
    }

    //////////////////////////////////////////////////

    // showing the panel with buttons settings
    public void ShowMIMG()
    {
        if (mimg_button != null)
            mimg_button.UnSelect();
        mimg_button = null;

        PanelButtons.SetActive(true);
        PanelEditScheme.SetActive(true);

        UpdateButtonsScheme();
        UpdatePanelMiddle();

        UpdatePrcSize();
        UpdatePrcTransparency();
    }

    // hide the panel
    public void HideMIMG()
    {
        PanelButtons.SetActive(false);
        PanelEditScheme.SetActive(false);
        PanelSchemes.SetActive(false);
        PanelQReset.SetActive(false);
        PanelQSave.SetActive(false);
    }

    //////////////////////////////////////////////////

    public void ClickButtonSchemes()
    {
        PanelSchemes.SetActive(true);
        UpdatePanelSchemes();
    }

    public void ClickButtonExit()
    {
        if(IsEditButtons)
            PanelQSave.SetActive(true);
        else
            HideMIMG();
    }

    public void ClickButtonReset()
    {
        PanelQReset.SetActive(true);
    }

    public void ClickButtonSave()
    {
        Save();
    }

    //////////////////////////////////////////////////

    public void OnValueChangedSliderSize()
    {
        UpdatePrcSize();

        if(mimg_button != null)
            mimg_button.Size = SliderSize.value;

        EditingScheme();
    }

    public void OnValueChangedSliderTransparency()
    {
        UpdatePrcTransparency();

        if (mimg_button != null)
            mimg_button.Transparency = SliderTransparency.value;

        EditingScheme();
    }

    public void ClickButtonMoveUp()
    {
        if (mimg_button != null)
            mimg_button.Position += Vector2.up * k_move;

        EditingScheme();
    }

    public void ClickButtonMoveDown()
    {
        if (mimg_button != null)
            mimg_button.Position += Vector2.down * k_move;

        EditingScheme();
    }

    public void ClickButtonMoveLeft()
    {
        if (mimg_button != null)
            mimg_button.Position += Vector2.left * k_move;

        EditingScheme();
    }

    public void ClickButtonMoveRight()
    {
        if (mimg_button != null)
            mimg_button.Position += Vector2.right * k_move;

        EditingScheme();
    }

    public void ClickButtonBottom()
    {
        PanelMiddle.SetActive(!PanelMiddle.activeSelf);
        UpdatePanelMiddle();
    }

    //////////////////////////////////////////////////

    public void SchemesClickButtonClose()
    {
        PanelSchemes.SetActive(false);
    }

    public void SchemesOnEndEditInputFieldSchemes1()
    {
        SetNameScheme(1, InputFieldSchemes1.text);
        UpdateButtonsScheme();
    }

    public void SchemesClickButtonSchemes1Use()
    {
        SetUseIdScheme(1);
        UpdatePanelSchemes();
        UpdateButtonsScheme();
        UpdateGameButtons();
    }

    public void SchemesOnEndEditInputFieldSchemes2()
    {
        SetNameScheme(2, InputFieldSchemes2.text);
        UpdateButtonsScheme();
    }

    public void SchemesClickButtonSchemes2Use()
    {
        SetUseIdScheme(2);
        UpdatePanelSchemes();
        UpdateButtonsScheme();
        UpdateGameButtons();
    }

    public void SchemesOnEndEditInputFieldSchemes3()
    {
        SetNameScheme(3, InputFieldSchemes3.text);
        UpdateButtonsScheme();
    }

    public void SchemesClickButtonSchemes3Use()
    {
        SetUseIdScheme(3);
        UpdatePanelSchemes();
        UpdateButtonsScheme();
        UpdateGameButtons();
    }

    //////////////////////////////////////////////////

    public void QResetClickButtonClose()
    {
        PanelQReset.SetActive(false);
    }

    public void QResetClickButtonCancel()
    {
        QResetClickButtonClose();
    }

    public void QResetClickButtonReset()
    {
        ResetScheme();
        EditingScheme();
        QResetClickButtonClose();
    }

    //////////////////////////////////////////////////

    public void QSaveClickButtonClose()
    {
        PanelQSave.SetActive(false);
    }

    public void QSaveClickButtonDoNotSave()
    {
        HideMIMG();
    }

    public void QSaveClickButtonSave()
    {
        ClickButtonSave();
        HideMIMG();
    }

    //////////////////////////////////////////////////
    //////////////////////////////////////////////////

    // Save the circuit in the game
    void Save()
    {
        int id = GetSelectIdScheme();
        List<SaveButton> _scheme = new List<SaveButton>();

        foreach (MIMG_button bu in _MIMG_button)
        {
            _scheme.Add(new SaveButton()
            {
                buttonsGame = bu.buttonsGame,
                m_Position = bu.Position,
                m_Size = bu.Size,
                m_Transparency = bu.Transparency
            });
        }

        SetScheme(_scheme, id);

        IsEditButtons = false;

        UpdateGameButtons();
    }

    // Loading the scheme in the game
    void Load()
    {
        List<SaveButton> _scheme = GetNowScheme();

        foreach (MIMG_button bu in _MIMG_button)
        {
            foreach (SaveButton sb in _scheme)
            {
                if (bu.buttonsGame == sb.buttonsGame)
                {
                    bu.Position = sb.m_Position;
                    bu.Size = sb.m_Size;
                    bu.Transparency = sb.m_Transparency;

                    break;
                }
            }
        }

        IsEditButtons = false;
    }

    // Set the default scheme
    void ResetScheme()
    {
        int id = GetSelectIdScheme();
        List<SaveButton> _scheme = GetDefaultScheme(id);
        
        foreach (MIMG_button bu in _MIMG_button)
        {
            foreach (SaveButton sb in _scheme)
            {
                if (bu.buttonsGame == sb.buttonsGame)
                {
                    bu.Position = sb.m_Position;
                    bu.Size = sb.m_Size;
                    bu.Transparency = sb.m_Transparency;

                    break;
                }
            }
        }
    }

    // Save default schema
    public void SaveDefaultScheme(int id)
    {
        List<SaveButton> _scheme = new List<SaveButton>();

        foreach (MIMG_button bu in PanelButtons.GetComponentsInChildren<MIMG_button>())
        {
            _scheme.Add(new SaveButton()
            {
                buttonsGame = bu.buttonsGame,
                m_Position = bu.Position,
                m_Size = bu.Size,
                m_Transparency = bu.Transparency
            });
        }

        SetScheme(_scheme, id, true);
    }

    // Loading default scheme
    public void LoadDefaultScheme(int id)
    {
        List<SaveButton> _scheme = GetScheme(id, true);

        foreach (MIMG_button bu in PanelButtons.GetComponentsInChildren<MIMG_button>())
        {
            foreach(SaveButton sb in _scheme)
            {
                if(bu.buttonsGame == sb.buttonsGame)
                {
                    bu.Position = sb.m_Position;
                    bu.Size = sb.m_Size;
                    bu.Transparency = sb.m_Transparency;

                    break;
                }
            }
        }
    }

    // path to file
    public static string getPath(int id, bool HasDefault, bool HasResources = false)
    {
        string r = "Schemes" + (HasDefault ? "Default" : "") + "/";

        if (!HasResources)
        {
            r = Application.persistentDataPath + @"/MIMG/Resources/" + r;
            //r = Application.dataPath + @"/MIMG/Resources/" + r;

            if (!Directory.Exists(r))
            {
                Directory.CreateDirectory(r);
            }

            return Path.Combine(r, id + ".txt");
        }
        else
            return Path.Combine(r, id.ToString());
    }

    public static List<SaveButton> GetDefaultScheme(int id)
    {
        List<SaveButton> _scheme = new List<SaveButton>();
        XmlSerializer serializer = new XmlSerializer(typeof(List<SaveButton>));
        TextAsset textAsset = (TextAsset)Resources.Load(getPath(id, true, true));
        StringReader stread = new StringReader(textAsset.text);
        try
        {
            _scheme = (List<SaveButton>)serializer.Deserialize(stread);
        }
        catch { }
        stread.Close();

        return _scheme;
    }

    // loading from file
    public static List<SaveButton> GetScheme(int id, bool HasDefault = false)
    {
        List<SaveButton> _scheme = new List<SaveButton>();
        string file_name = getPath(id, HasDefault);
        XmlSerializer serializer = new XmlSerializer(typeof(List<SaveButton>));
        FileStream stream = new FileStream(file_name, FileMode.OpenOrCreate);
        TextReader textReader = new StreamReader(stream, Encoding.UTF8);
        try
        {
            _scheme = (List<SaveButton>)serializer.Deserialize(textReader);
        }
        catch { }
        textReader.Close();
        stream.Dispose();
        stream.Close();

        return _scheme;
    }

    // save in file
    public static void SetScheme(List<SaveButton> _scheme, int id, bool HasDefault = false)
    {
        string file_name = getPath(id, HasDefault);
        if (File.Exists(file_name))
            File.Delete(file_name);

        XmlSerializer serializer = new XmlSerializer(_scheme.GetType());
        FileStream stream = new FileStream(file_name, FileMode.Create);
        TextWriter textWriter = new StreamWriter(stream, Encoding.UTF8);
        serializer.Serialize(textWriter, _scheme);
        textWriter.Close();
        stream.Dispose();
        stream.Close();
    }

    // We return the current scheme
    public static List<SaveButton> GetNowScheme()
    {
        int id = GetSelectIdScheme();
        List<SaveButton> _scheme = GetScheme(id);
        if (_scheme.Count == 0)
            _scheme = GetDefaultScheme(id);

        return _scheme;
    }

    //////////////////////////////////////////////////

    // Updating the panel with schemes
    void UpdatePanelSchemes()
    {
        TextSchemesUse.text = GetNameScheme(GetSelectIdScheme());

        InputFieldSchemes1.text = GetNameScheme(1);
        InputFieldSchemes2.text = GetNameScheme(2);
        InputFieldSchemes3.text = GetNameScheme(3);

        GOSchemes1Use.SetActive(true);
        GOSchemes1Used.SetActive(false);
        GOSchemes2Use.SetActive(true);
        GOSchemes2Used.SetActive(false);
        GOSchemes3Use.SetActive(true);
        GOSchemes3Used.SetActive(false);

        switch (GetSelectIdScheme())
        {
            case 1:
                GOSchemes1Use.SetActive(false);
                GOSchemes1Used.SetActive(true);
                break;
            case 2:
                GOSchemes2Use.SetActive(false);
                GOSchemes2Used.SetActive(true);
                break;
            case 3:
                GOSchemes3Use.SetActive(false);
                GOSchemes3Used.SetActive(true);
                break;
        }
    }

    // update the button that hides / shows the panel with sliders
    void UpdatePanelMiddle()
    {
        IamgeBottom.sprite = PanelMiddle.activeSelf ? SpriteBottomOpen : SpriteBottomClose;
    }

    // updating the panel with buttons for editing
    void UpdateButtonsScheme()
    {
        TextNameScheme.text = GetNameScheme(GetSelectIdScheme());

        Load();
    }

    // update the percentage of size
    void UpdatePrcSize()
    {
        float prc = SliderSize.value * 100;

        TextPrcSize.text = ((int)prc).ToString() + "%";
    }

    // updating transparency percentage
    void UpdatePrcTransparency()
    {
        float prc = SliderTransparency.value * 100;

        TextPrcTransparency.text = ((int)prc).ToString() + "%";
    }

    // parameters from the selected button
    public void SelectButton(MIMG_button _mimg_button)
    {
        if (mimg_button != null)
            mimg_button.UnSelect();
        mimg_button = _mimg_button;

        SliderSize.value = mimg_button.Size;
        SliderTransparency.value = mimg_button.Transparency;

        UpdatePrcSize();
        UpdatePrcTransparency();
    }

    // if there were changes
    public void EditingScheme()
    {
        IsEditButtons = true;
    }

    // update the buttons in the game
    void UpdateGameButtons()
    {
        if (MIMG_game._MIMG_game != null)
        {
            MIMG_game._MIMG_game.UpdateNowScheme();
        }
    }

    //////////////////////////////////////////////////

    // we return the number of the current selected scheme
    public static int GetSelectIdScheme()
    {
        return PlayerPrefs.GetInt("SelectIdScheme", 1);
    }

    // set the circuit number
    void SetUseIdScheme(int id)
    {
        PlayerPrefs.SetInt("SelectIdScheme", id);
    }

    // we return the name of the scheme by number
    string GetNameScheme(int id)
    {
        return PlayerPrefs.GetString("NameScheme" + id, "Scheme " + id);
    }

    // set the schema name
    void SetNameScheme(int id, string name)
    {
        PlayerPrefs.SetString("NameScheme" + id, name);
    }
}
