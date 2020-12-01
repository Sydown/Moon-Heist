using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force;
    private bool isFlying;
    private GameObject smokeParticles;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        smokeParticles = GameObject.Find("Smoke Particles");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isFlying = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isFlying = false;
        }
    }

    private void FixedUpdate()
    {
        if (isFlying)
        {
            rb.velocity = new Vector2(rb.velocity.x, force);
            smokeParticles.SetActive(true);
        }
        else
        {
            smokeParticles.SetActive(false);
        }
    }

    public bool FlightStatus()
    {
        return isFlying;
    }
}
