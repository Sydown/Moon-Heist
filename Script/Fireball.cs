using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Vector3 relativePos;
    private Transform target;
    public float speed;
    private Rigidbody2D rb;
    private Animator animator;
    private bool hit;
    public float offset;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

        if (GameObject.FindGameObjectWithTag("Player")== null)
        {
            Destroy(gameObject);
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            relativePos = target.position - transform.position;
            float rotZ = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!hit)
        {
            rb.velocity = (relativePos.normalized * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Platform"))
        {
            animator.SetTrigger("Explode");
            hit = true;
            rb.velocity = Vector2.zero;
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerDeath>().Die();
            }
        }
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
