using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NGBox : MonoBehaviour
{
    public GameObject Fire;
    // Start is called before the first frame update
    void Start()
    {
        Fire = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Arrow")
        {
            print("1111");
            Fire.SetActive(true);
        }
    }
}
