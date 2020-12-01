using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearStageButton : MonoBehaviour
{
    public float duration;
    private StageRequirement stageReq;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(LerpScale(new Vector3(1, 1, 0), duration));
        if (GameObject.FindObjectOfType<StageRequirement>() != null)
        {
            stageReq = GameObject.FindObjectOfType<StageRequirement>().GetComponent<StageRequirement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (transform.localScale == new Vector3(1, 1, 0))
            {
                    animator.SetTrigger("isPressed"); 
            }
        }
    }

    void ButtonPressed()
    {
        if (stageReq != null)
        {
            StopAllCoroutines();
            StartCoroutine(LerpScale(Vector3.zero, duration));
            StartCoroutine(stageReq.SpawnButton(transform.position));
        }
        else
        {
            StartCoroutine(GameManager.instance.LoadNextScene());
        }
    }

    IEnumerator LerpScale(Vector3 targetScale, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
