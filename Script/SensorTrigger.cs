using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorTrigger : MonoBehaviour
{
    private GameObject barrier;
    public float moveDirection;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        barrier = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("SensorActivate");
            ActivateBarrier();
        }
    }

    void ActivateBarrier()
    {
        Vector3 targetPos = new Vector3(barrier.transform.position.x + moveDirection, barrier.transform.position.y, barrier.transform.position.z);
        StartCoroutine(LerpPosition(targetPos, 3));
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            barrier.transform.position = Vector3.Lerp(barrier.transform.position, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        barrier.transform.position = targetPosition;
    }
}
