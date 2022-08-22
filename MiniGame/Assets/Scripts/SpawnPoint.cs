using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject character;
    public GameObject characterPerfab;

    private GameObject self;
    private void Awake()
    {
        self = gameObject;
        characterPerfab=Instantiate(character, self.transform.position,self.transform.rotation);
        self.GetComponent<SpriteRenderer>().enabled = false;
    }//生成角色
}