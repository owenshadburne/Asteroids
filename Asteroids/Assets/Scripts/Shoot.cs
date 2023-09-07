using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] AudioSource clip;
    [SerializeField] GameObject bullet;
    GameObject tip;

    [SerializeField] Animator animator;

    private void Start()
    {
        tip = GameObject.FindGameObjectWithTag("Tip");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        GameObject temp = Instantiate(bullet, tip.transform.position, transform.rotation);
        temp.transform.eulerAngles = transform.rotation.eulerAngles;
        animator.SetTrigger("Shoot");
        clip.Play();
    }
}
