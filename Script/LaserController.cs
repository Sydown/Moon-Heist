using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public List<GameObject> lasers;
    public float startTimeBtwAttack;
    private float timeBtwAttack;
    private bool attacking;
    // Start is called before the first frame update
    void Start()
    {
       // lasers = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack <= 0)
        {
            Attack();
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void Attack()
    {
        foreach (GameObject laser in lasers)
        {
            laser.GetComponent<LaserBeam>().SetWarning();
        }

    }

}

