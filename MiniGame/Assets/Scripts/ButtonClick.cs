using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public GameObject MenuPausing;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Testing_Scene_1");
    }
    public void Continue()
    {
        SceneManager.LoadScene("Testing_Scene_1");
    }
    public void ExitGame()
    {
        //SceneManager.LoadScene("StartSCene");
        Application.Quit();
    }
    public void Settings()
    {

    }
    public void Pause()
    {
        MenuPausing.SetActive(true);
    }

    public void ContinueInGame()
    {
        MenuPausing.SetActive(false);
    }
    public void Test()
    {
        print("TEST");
    }

    public void Save()
    {
       var player= GameObject.FindWithTag("Player");
       player.GetComponent<Character>().Save();
       Debug.Log("Save");
    }
}
