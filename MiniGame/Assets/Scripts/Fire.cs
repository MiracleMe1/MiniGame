using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private int  i = 0;
    public bool isOnFire=false;
    public bool FireEventTrigger = false;
    public GameObject[] nearBlock=new GameObject[20];
    public int Timer = 100;
    
    private void FixedUpdate()
    {
        
        if (isOnFire)
        {
            Timer--;
            if (Timer<=0)
            {
                if (!FireEventTrigger)
                {
                  FireEventTrigger = true;
                  GetComponent<SpriteRenderer>().color=Color.red;  
                }

                foreach (var o in nearBlock)
                {
                    if (o == null)
                    {
                        break;
                    }
                    o.GetComponent<Fire>().isOnFire = true;
                    if (!o.GetComponent<Fire>().FireEventTrigger)
                    { 
                        FireEventTrigger = true;
                    }
                    
                }
                Timer = 100; 
                
            }

        }

    }
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Arrow"))
        {
            if (other.GetComponent<Fire>()!=null)
            {
                Debug.Log("trigger");
                Debug.Log(other.gameObject);
                bool re=false;
                foreach (var o in nearBlock)
                {
                    if (o == other.gameObject)
                    {
                        re = true;
                        break;
                    }
                }

                if (!re)
                {
                    nearBlock[i] = other.gameObject;
                    i++;
                }
                Debug.Log(i);
            }
            
        }
       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        bool re=false;
        int num = 0;
        int last = nearBlock.Length - 1;
        foreach (var o in nearBlock)
        {
            if (o == other.gameObject)
            {
                re = true;
                break;
            }

            num++;
        }

        for (int j = num; j < nearBlock.Length-1; j++)
        {
            nearBlock[num] = nearBlock[num + 1];
        }

        nearBlock[last] = null;
        i--;
        if (i < 0)
        {
            i = 0;
            nearBlock.Initialize();
        }
        Debug.Log(i);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        /*if (other.collider.CompareTag("Arrow"))
        {
            Debug.Log("hit");
            isOnFire = true;
        }*/
        if (other.collider.gameObject.CompareTag("Arrow"))
        {
            if (other.collider.GetComponent<Arrow>().isOnFire)
                    {
                        isOnFire = true;
                        Debug.Log("OnFire");
                    }
        }
        
    }

    public bool GetIsFire()
    {
        return isOnFire;
    }
}
