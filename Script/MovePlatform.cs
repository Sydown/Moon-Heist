using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private Vector3 targetPos, originPos;
    private bool atOrigin;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        targetPos = gameObject.transform.GetChild(0).position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, targetPos) >= 0.2 && atOrigin)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else
        {
            atOrigin = false;
            transform.position = Vector3.MoveTowards(transform.position, originPos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, originPos) <= 0.2)
            {
                atOrigin = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
