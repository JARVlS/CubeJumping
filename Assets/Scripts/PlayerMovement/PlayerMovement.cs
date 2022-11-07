using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] public float speed;
    [SerializeField] public float jumpPower;
    [SerializeField] private LayerMask groundLayer;


    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizonalInput;

    private Vector3 playerScale;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizonalInput = Input.GetAxis("Horizontal");


        body.velocity = new Vector2(horizonalInput * speed, body.velocity.y);
        
        playerScale = transform.localScale;

        if(horizonalInput > 0.01f){
            transform.localScale = new Vector3(0.3f, playerScale.y, playerScale.z);
        }
        else if(horizonalInput<-0.01f){
            transform.localScale = new Vector3(-0.3f, playerScale.y, playerScale.z);
        }
        
        anim.SetBool("grounded", isGrounded());
        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (isGrounded())
        {
            anim.SetTrigger("jump");
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
        // else if(jumpCounter>0)
        // body.velocity = new Vector2(body.velocity.x, jumpPower);
    }

    private bool isGrounded()
    {
        // box beneath player to check if anything in current layer beneath it
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
