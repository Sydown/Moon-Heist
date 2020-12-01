using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apollo : MonoBehaviour
{
    private Rigidbody2D rb;
    private float direction, startTime;
    public float maxLaunchForce, speed;
    private float objectWidth;
    private Vector3 bottomLeft, upperRight;
    private bool isGrounded, canJump;
    public float distance;
    public PhysicsMaterial2D bouncy;
    public LayerMask layer;
    private SpriteMask powerMask;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        powerMask = GameObject.FindObjectOfType<SpriteMask>();
        objectWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        bottomLeft = Camera.main.ViewportToWorldPoint(Vector3.zero);
        upperRight = Camera.main.ViewportToWorldPoint(new Vector3(1,1,0));
    }

    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down,distance,layer);
        if (isGrounded && rb.velocity == Vector2.zero)
        {
            rb.sharedMaterial = null;
            if (Input.GetMouseButtonDown(0))
            {
                startTime = Time.time;
                canJump = true;
            }

            if (Input.GetMouseButton(0) && canJump)
            {
                float holdTime = Time.time - startTime;
                ShowForce(HoldDownForce(holdTime));
            }

            if (Input.GetMouseButtonUp(0) && canJump)
            {  
                float holdDownTime = Time.time - startTime;
                HideForce();
                Launch(HoldDownForce(holdDownTime));
                canJump = false;
            }
        }
        else
        {
            rb.sharedMaterial = bouncy;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && isGrounded)
        {
            rb.velocity = Vector2.zero;
        }
    }

    /* private void LateUpdate()
     {
         Vector3 currentPos = transform.position;
         currentPos.x = Mathf.Clamp(transform.position.x, bottomLeft.x + objectWidth, upperRight.x - objectWidth);
         transform.position = currentPos;
     }*/

    float HoldDownForce(float holdTime)
    {
        float maxHoldTime = 2f;
        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxHoldTime);
        float launchForce = holdTimeNormalized * maxLaunchForce;
        return launchForce;


    }

    void Launch(float force)
    { 
            Vector2 dir = (GetMouseWorldPosition() - transform.position).normalized;
            Vector2 launchForce = dir * force;
            rb.velocity = launchForce;

    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePos;
    }

    private void ShowForce(float force)
    {
        powerMask.alphaCutoff = 1 -( force / maxLaunchForce);
    }

    private void HideForce()
    {
        powerMask.alphaCutoff = 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - distance));
    }

}
