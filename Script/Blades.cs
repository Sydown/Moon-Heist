using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blades : MonoBehaviour
{
    public float speed;
    private Vector3 originPos;
    private Vector3 targetPos;
    private bool goingBack;
    // Start is called before the first frame update
    void Start()
    {
        targetPos = gameObject.transform.GetChild(0).transform.position;
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            if (Vector3.Distance(transform.position, targetPos) >= 0.5 && !goingBack)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }
            else
            {
                GoBack();
            }
        }

    }

    private void GoBack()
    {
        goingBack = true;
        transform.position = Vector3.MoveTowards(transform.position, originPos, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, originPos) <= 0.5)
        {
            goingBack = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerDeath>().Die();
        }
    }
}

