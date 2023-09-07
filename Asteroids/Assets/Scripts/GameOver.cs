using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;
    bool stopped;

    [SerializeField] GameObject ship;
    [SerializeField] TextMeshProUGUI score, gameOverText;
    float scr;

    void Awake()
    {
        instance = this;
        gameOverText.enabled = false;
        score.text = "" + 0;
    }

    public void Restart()
    {
        foreach(GameObject o in GameObject.FindGameObjectsWithTag("Spawn"))
        {
            o.GetComponent<Spawner>().Stop();
        }
        stopped = true;
        gameOverText.enabled = true;
    }

    private void Update()
    {
        if (!stopped) { scr += Time.deltaTime; }
        score.text = "" + (int)scr;
        if(stopped && Input.GetKeyDown(KeyCode.R) && ScreenWipe.instance.Complete())
        {
            Instantiate(ship);
            foreach (GameObject o in GameObject.FindGameObjectsWithTag("Spawn"))
            {
                o.GetComponent<Spawner>().Restart();
            }
            stopped = false;
            scr = 0;
            gameOverText.enabled = false;
        }
    }
}
