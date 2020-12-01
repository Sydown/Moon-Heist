using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : MonoBehaviour
{
    private Vector3 targetPos;
    public float speed;
    private BombArea bombArea;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        bombArea = gameObject.transform.parent.GetComponent<BombArea>();
        targetPos = bombArea.GetRandomCoords();

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, targetPos) >= 0.5)
        {

            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else
        {
            targetPos = bombArea.GetRandomCoords();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            targetPos = bombArea.GetRandomCoords();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Explode");
            collision.gameObject.GetComponent<PlayerDeath>().Die();
        }
    }

    private void DestroyBomb()
    {
        Destroy(this.gameObject);
    }



}
