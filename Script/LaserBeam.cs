using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }
    public void SetWarning()
    {
        animator.SetTrigger("Warning");
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(4);
        animator.SetTrigger("Stop");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerDeath>().Die();
        }
    }
}
