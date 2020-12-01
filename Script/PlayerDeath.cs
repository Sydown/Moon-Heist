using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Flight flight;
    private Animator animator;
    private Rigidbody2D rb;
    public float force;
    private bool isDead = false;
    private Vector3 bottomScreen;
    public float rotationSpeed;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        flight = GetComponent<Flight>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Die();
           // animator.SetTrigger("isDead");
        }

        if (isDead)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            if (bottomScreen.y > transform.position.y)
            {
                StartCoroutine(GameManager.instance.RestartLevel());
            }
        }
    }

    public void Die()
    {
        //Death animation
        DeathAnim();
        // stop all other components
        playerMovement.enabled = false;
        flight.enabled = false;
        // lock the camera
        // restartLevel
    }

    public void DeathAnim()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isFlying", false);
        rb.velocity = Vector2.zero;
        isDead = true;
        bottomScreen = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
