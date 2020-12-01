using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSpaceman : MonoBehaviour
{
  
    private Vector3 targetPos;
    public float speed;
    private BombArea bombArea;
    // Start is called before the first frame update
    void Start()
    {
        bombArea = gameObject.transform.parent.GetComponent<BombArea>();
        targetPos = bombArea.GetRandomCoords();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 50 * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPos) >= 0.5)
        {

            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else
        {
            targetPos = bombArea.GetRandomCoords();
        }
    }
}
