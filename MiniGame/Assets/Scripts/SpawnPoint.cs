using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject character;
    public GameObject characterPerfab;
    private BoxCollider2D collider2d;
    public int numOfCheckPoint;
    
    private GameObject self;
    
    private void Awake()
    {
        self = gameObject;
        collider2d = GetComponent<BoxCollider2D>();
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
            collider2d.enabled = false;
            characterPerfab=Instantiate(character, self.transform.position,self.transform.rotation);
            self.GetComponent<SpriteRenderer>().enabled = false;
            SaveSystem.LoadByPlayerPrefs("PlayerData");
            Debug.Log("Ana");
        }
        else
        {
            collider2d.enabled = true;
            self.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}