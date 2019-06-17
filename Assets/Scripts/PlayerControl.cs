using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    CapsuleCollider2D cc2d;
    float gravityScaleStart;
    BoxCollider2D feet;

    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(20f,20f);

    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cc2d = GetComponent<CapsuleCollider2D>();
        feet = GetComponent<BoxCollider2D>();
        gravityScaleStart = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        Jump();
        Flip();
        Climb();
        Death();
    }

    void Run()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 runVelocity = new Vector2(horizontal*runSpeed, rb.velocity.y);
        rb.velocity = runVelocity;
        bool hasHorSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        anim.SetBool("Running", hasHorSpeed);
    }

    void Climb()
    {
        if(!feet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb.gravityScale = gravityScaleStart;
            anim.SetBool("Climbing", false);
            return;
        }
        float vertical = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(rb.velocity.x, vertical * climbSpeed);
        rb.velocity = climbVelocity;
        rb.gravityScale = 0f;

        bool hasVSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        anim.SetBool("Climbing", hasVSpeed);

    }

    void Jump()
    {
        if (!feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        { return; }

            if (Input.GetButtonDown("Jump"))
            {
                Vector2 jumpVelocity = new Vector2(0, jumpSpeed);
                rb.velocity += jumpVelocity;
            }
       
    }

    void Flip()
    {
        bool hasHorSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if(hasHorSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1);
        }
    }

    void Death()
    {
        if(cc2d.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards")))
        {
            isAlive = false;
            anim.SetTrigger("Dying");
            rb.velocity = deathKick;
            GameManager.instance.PlayerDeathProcess();
        }
    }
}
