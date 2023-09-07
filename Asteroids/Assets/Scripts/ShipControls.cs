using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShipControls : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject deathParticle;
    float lives = 1;
    Rigidbody2D rb;

    Vector2 movement;
    float speed = 10, maxSpeed = 5,
        friction = .01f, rotationAmount = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float vert = Input.GetAxisRaw("Vertical");
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), vert > 0 ? vert : 0);
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.up * speed * movement.y);
        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        if(movement == Vector2.zero)
        {
            rb.velocity -= rb.velocity.normalized * friction;
        }
        rb.MoveRotation(rb.rotation + (-movement.x * rotationAmount));
    }

    public void OnDeath()
    {
        //Reset position
        lives--;
        animator.Play("Death");

        if (lives <= 0)
        {
            Camera.main.GetComponents<AudioSource>()[1].Play();
            GameOver.instance.Restart();
            lives = 1;
        }
    }

    public void Dest()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
