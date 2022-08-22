using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowForTesting: MonoBehaviour
{

    public GameObject character;

    private void Start()
    {
        character = GameObject.Find("Character");
    }

    private void Update()
    {
        gameObject.transform.position = character.transform.position + new Vector3(1, 0, -10);
    }
}
