using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWipe : MonoBehaviour
{
    public static ScreenWipe instance;
    float timer, maxSize = 6f;
    float multi = 3;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        timer = maxSize / multi;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        transform.localScale = new Vector2(maxSize - timer * multi, maxSize - timer * multi);

        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

    public bool Complete()
    {
        return timer <= 0;
    }
}
