using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 5;
    float lifetime = 1f;
    bool hasLifetime, hasWrapped;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hasWrapped = false;
    }

    private void Update()
    {
        Life();        
    }
    void Life()
    {
        if (hasLifetime)
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = speed * transform.up;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Asteroid"))
        {
            collision.GetComponent<Asteroid>().OnHit();
            Destroy(gameObject);
        }
        else if(hasWrapped && collision.gameObject.CompareTag("Ship"))
        {
            collision.GetComponent<ShipControls>().OnDeath();
            Destroy(gameObject);
        }
    }

    public void HasWrapped()
    {
        hasWrapped = true;
    }
}
