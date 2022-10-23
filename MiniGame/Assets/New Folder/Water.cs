using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public RaycastHit2D Right;
    public GameObject rightHitResult;
    public RaycastHit2D[] _raycastHit2Ds=new RaycastHit2D[10];
    public GameObject leftCheck;
    public GameObject rightCheck;
    public GameObject upCheck;
    public GameObject downCheck;
    
    public string[] nameArray = new string[10];
    private BoxCollider2D _selfBoxCollider2D;
    private int i = 0;
    

    private void Start()
    {
        _selfBoxCollider2D = GetComponent<BoxCollider2D>();
        
    }

    
}
