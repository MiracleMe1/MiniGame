using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Bow : MonoBehaviour
{
    public Joystick joystick;
    public GameObject arrow;
    public float launchForce=10f;
    public Transform shotPosition;
    public GameObject point;
    private GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;
    public bool onFire = false;
    
    private float zRotation=0f;
    private float isDrag = 0f;
    private Vector2 dragDirection = new Vector2(0, 0);
    

    void Start()
    {
        joystick=GameObject.Find("ImageEdgeStick").GetComponent<Joystick>();
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPosition.position, Quaternion.identity);
        }//初始化瞄准线
    }
    
    void Update()
    {
        isDrag = joystick.isDrag;
        ZRotationValue();
        transform.rotation = Quaternion.Euler(0, 0, zRotation+180f*isDrag);
        for (int i = 0; i < numberOfPoints; i++)//画出瞄准线
        {
            points[i].transform.position = PointPosition(i * spaceBetweenPoints);
            if (isDrag == 1f)
            {
                points[i].GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                points[i].GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        
    }

    //获得UI拖拽的角度与力度
    private void ZRotationValue()
    {
        dragDirection = -joystick.dragDirection;
        zRotation = Mathf.Atan2(joystick.dragDirection.y,joystick.dragDirection.x)*Mathf.Rad2Deg;
        launchForce = joystick.dragStrength.magnitude/6f;
    }
    //射击
    public void Shoot()
    {
        GameObject newArrow = Instantiate(arrow, shotPosition.position, shotPosition.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = dragDirection * launchForce;
        if (onFire)
        {
            newArrow.GetComponent<Arrow>().SetStateOfArrow(onFire);
        }
        else if (!onFire)
        {
            newArrow.GetComponent<Arrow>().SetStateOfArrow(onFire);
        }
            Debug.Log(launchForce);
    }

    Vector2 PointPosition(float t)
    {
        Vector2 pos = (Vector2) shotPosition.position + (dragDirection * launchForce * t) + 0.5f * Physics2D.gravity * t * t;
        return pos;
    }
}

