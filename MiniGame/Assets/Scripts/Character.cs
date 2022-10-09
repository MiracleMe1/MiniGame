using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.Rendering.Universal;

public class Character : MonoBehaviour
{
    private int health;
    private int numberOfWoodArrow;
    private int numberOfFlinstone;
    private int World;
    public int currentCheckPoint;

    public int GetHealth()
    {
        return health;
    }
    public int GetNumberOfWoodArrow()
    {
        return numberOfWoodArrow;
    }
    public int GetNumberOfFlinstone()
    {
        return numberOfFlinstone;
    }

    [System.Serializable]
    class SaveData
    {
        public int World;
        public int currentCheckPoint;
        public int numOfFlinstone;
    }
    private void Start()
    {
        gameObject.GetComponentInChildren<CinemachineConfiner>().m_BoundingShape2D =
            GameObject.Find("LevelArea").GetComponent<PolygonCollider2D>();
       // gameObject.GetComponent<PlayerInput>().uiInputModule =
           // GameObject.Find("EventSystem").GetComponent<InputSystemUIInputModule>();
    }

    public void SetWorld()
    {
        
    }

    public void SetCurrentCheckPoint(int current)
    {
        currentCheckPoint = current;
    }

    public void Save()
    {
        var saveData = new SaveData();
        saveData.World = World;
        saveData.currentCheckPoint = currentCheckPoint;
        saveData.numOfFlinstone = numberOfFlinstone;
        
        SaveSystem.SaveByPlayerPrefs("PlayerData",saveData);
    }

    public void Load()
    {
        var json = SaveSystem.LoadByPlayerPrefs("PlayerData");
        var saveData = JsonUtility.FromJson<SaveData>(json);
        World = saveData.World;
        currentCheckPoint = saveData.currentCheckPoint;
        numberOfFlinstone = saveData.numOfFlinstone;
    }
    
    [UnityEditor.MenuItem("Developer/Delete All Player Data Prefs")]
    public static void DeletePlayerDataPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
