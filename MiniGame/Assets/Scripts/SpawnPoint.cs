using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject character;
    public GameObject characterPerfab;
    private BoxCollider2D collider;
    public int numOfCheckPoint;
    
    private GameObject self;
    
    private void Awake()
    {
        self = gameObject;
        collider = GetComponent<BoxCollider2D>();
        SpawnPlayer(numOfCheckPoint);

    }//生成角色

    private void Start()
    {
        characterPerfab=GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        characterPerfab.GetComponent<Character>().SetCurrentCheckPoint(numOfCheckPoint);
        Debug.Log("Trigger!");
    }

    public void SpawnPlayer(int numOfCheckPoint)
    {
        if (numOfCheckPoint == 0)
        { 
            collider.enabled = false;
            characterPerfab=Instantiate(character, self.transform.position,self.transform.rotation);
            self.GetComponent<SpriteRenderer>().enabled = false;
            SaveSystem.LoadByPlayerPrefs("PlayerData");
            Debug.Log("Ana");
        }
        else
        {
            collider.enabled = true;
            self.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}