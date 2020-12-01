using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{
    public Vector3 offset;
    public float attackRange;
    private GameObject player;
    public float startTimeBtwAttack;
    private float timeBtwAttack;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //if player is in attack range, instantiate bullets every 3 seconds
        if (Vector3.Distance(transform.position+ offset, player.transform.position) <= attackRange)
        {
            if (timeBtwAttack <= 0)
            {
                //Spawn bullets
                Instantiate(bullet, transform.position, Quaternion.identity);
                timeBtwAttack = startTimeBtwAttack;
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + offset, attackRange);
    }
}
