using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private float angle = 0f;
    private bool isHit = false;
    private float lifeTime = 1f;

    public GameObject Self;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit == false)
        {
            angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation=Quaternion.AngleAxis(angle,Vector3.forward);
        }

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(Self);
            Debug.Log("Destroy");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         Debug.Log("isHit");
         isHit = true;
         rb.velocity = new Vector2(0, 0);
         rb.isKinematic = true;
    }
    
}
