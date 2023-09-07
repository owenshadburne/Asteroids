using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    //[SerializeField] AudioSource clip;
    [SerializeField] GameObject[] asteroids;
    [SerializeField] GameObject deathParticle;

    int health = 1;
    Size size;
    float scaler = .75f;

    const float maxSpeed = 2f, minSpeed = 1f,
        minTorque = 1, maxTorque = 2;
    Rigidbody2D rb;

    public void StartManual()
    {
        size = (Size) Random.Range((int)Size.MEDIUM, (int)Size.BIG + 1);
        Move();
        Scale();
    }

    void StartAsteroid(Size s)
    {
        size = s;
        Move();
        Scale();
    }
    void Move()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddTorque(Random.Range(minTorque, maxTorque) * (Random.Range(0, 1) < .5 ? 1 : -1), ForceMode2D.Impulse);
        rb.velocity = Quaternion.Euler(0, 0, Random.Range(0, 360)) * Vector2.up * Random.Range(minSpeed, maxSpeed);
    }
    void Scale()
    {
        if (size == Size.MEDIUM)
        {
            transform.localScale = new Vector3(transform.localScale.x * scaler, transform.localScale.y * scaler, transform.localScale.z * scaler);
        }
        else if (size == Size.SMALL)
        {
            transform.localScale = new Vector3(transform.localScale.x * scaler * scaler, transform.localScale.y * scaler * scaler, transform.localScale.z * scaler * scaler);
        }
    }

    public void OnHit()
    {
        health--;
        if (health <= 0)
        {
            AudioSource a = Camera.main.GetComponent<AudioSource>();
            a.time = .2f;
            a.Play();
            BreakApart();
        }
    }

    void BreakApart()
    {
        if (size != Size.SMALL)
        {
            Size nextSize = size == Size.BIG ? Size.MEDIUM : Size.SMALL;
            float rng = Random.Range(Mathf.PI / 6, Mathf.PI / 3);

            GameObject a1 = Instantiate(asteroids[Random.Range(0, asteroids.Length - 1)], transform.position, Quaternion.identity);
            a1.GetComponent<Asteroid>().StartAsteroid(nextSize);

            GameObject a2 = Instantiate(asteroids[Random.Range(0, asteroids.Length - 1)], transform.position, Quaternion.identity);
            a2.GetComponent<Asteroid>().StartAsteroid(nextSize);
        }

        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ship"))
        {
            collision.gameObject.GetComponent<ShipControls>().OnDeath();
        }
    }
}

public enum Size
{
    SMALL = 0,
    MEDIUM = 1,
    BIG = 2
}