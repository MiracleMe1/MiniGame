using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class testing : MonoBehaviour
{
    public Collider2D[] _collider2D;
    public Collider2D result;
    private ContactFilter2D fliter=new ContactFilter2D().NoFilter();
    private void Update()
    {
        result=Physics2D.OverlapBox(transform.position, new Vector2(1f,1f), 0f,10);
        
    }
}
