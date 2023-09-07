using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] asteroids;
    float timer, min = 5, max = 20;
    bool inside;
    bool stopped;

    void Start()
    {
        Spawn();
    }

    void Update()
    {
        if(!stopped)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Spawn();
            }
        }
    }

    void Spawn()
    {
        if(!inside)
        {
            GameObject a = Instantiate(asteroids[Random.Range(0, asteroids.Length - 1)], transform.position, Quaternion.identity);
            a.GetComponent<Asteroid>().StartManual();
        }
        
        timer = Random.Range(min, max);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inside = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inside = false;
    }

    public void Restart()
    {
        stopped = false;
        timer = 0;
    }

    public void Stop()
    {
        stopped = true;
    }
}
