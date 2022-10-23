using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public string[] Dialogs;
    public Text DilogText;
    public Image BackGroundImage;
    private int DialogNum;
    
    // Start is called before the first frame update
    void Start()
    {
        DialogNum = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            DilogText.text = Dialogs[DialogNum];
        }
        catch
        {
            if(BackGroundImage)
            {
                BackGroundImage.gameObject.SetActive(false);
            }
            gameObject.SetActive(false);
        }
        
    }

    public void DialogNext()
    {
        DialogNum += 1;
    }
}
